using Catalog.Listener.Commands;
using Catalog.Listener.Configurations;
using Catalog.Listener.Proxies;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Catalog.Listener
{
    class Program
    {
        const string appsettingFile = "appsettings.json";
        static AzureServiceBus azure { get; set; }
        static ApiUrls apiUrls { get; set; }
        static ICatalogProxy catalogProxy { get; set; }
      
        static IQueueClient queueClient = null;

        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(appsettingFile).Build();

            azure = configuration.GetSection("AzureServiceBus").Get<AzureServiceBus>();
            apiUrls = configuration.GetSection("ApiUrls").Get<ApiUrls>();

            catalogProxy = new CatalogHttpProxy(apiUrls);            

            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            queueClient = new QueueClient(azure.ConnectionString, azure.QueueName);

            Console.WriteLine("===========================");
            Console.WriteLine("Press ENTER key to exit");
            Console.WriteLine("===========================");

            // Register QueueClient's MessageHandler and receive messages in a loop
            RegisterOnMessageHandlerAndReceiveMessages();

            Console.ReadKey();

            await queueClient.CloseAsync();
        }

        static void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                // Maximum number of Concurrent calls to the callback `ProcessMessagesAsync`, set to 1 for simplicity.
                // Set it according to how many messages the application wants to process in parallel.
                MaxConcurrentCalls = 1,
                // Indicates whether MessagePump should automatically complete the messages after returning from User Callback.
                // False below indicates the Complete will be handled by the User Callback as in `ProcessMessagesAsync` below.
                AutoComplete = false
            };

            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        static async Task ProcessMessagesAsync(Message message, System.Threading.CancellationToken token)
        {
            // Process the message
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{System.Text.Encoding.UTF8.GetString(message.Body)}");
            var command = System.Text.Json.JsonSerializer.Deserialize<ProductInStockUpdateCommand>(System.Text.Encoding.UTF8.GetString(message.Body));
            await catalogProxy.UpdateStockAsync(command);
            // Complete the message so that it is not received again.
            // This can be done only if the queueClient is created in ReceiveMode.PeekLock mode (which is default).
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
            // Note: Use the cancellationToken passed as necessary to determine if the queueClient has already been closed.
            // If queueClient has already been Closed, you may chose to not call CompleteAsync() or AbandonAsync() etc. calls 
            // to avoid unnecessary exceptions.
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
