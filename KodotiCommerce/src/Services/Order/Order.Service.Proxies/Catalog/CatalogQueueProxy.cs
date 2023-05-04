using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;
using Order.Service.Proxies.Catalog.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Order.Service.Proxies.Catalog
{
    public class CatalogQueueProxy : ICatalogProxy
    {
        private readonly AzureServiceBus _azure;

        public CatalogQueueProxy(IOptions<AzureServiceBus> azure)
        {
            _azure = azure.Value;
        }
        public async Task UpdateStockAsync(ProductInStockUpdateCommand command)
        {
            var queueClient = new QueueClient(_azure.ConnectionString, _azure.QueueName);

            var content = new StringContent(
                JsonSerializer.Serialize(command), encoding: System.Text.Encoding.UTF8, "application/json");

            await queueClient.SendAsync(new Message(content.ReadAsByteArrayAsync().Result));

            await queueClient.CloseAsync();
        }
    }
}
