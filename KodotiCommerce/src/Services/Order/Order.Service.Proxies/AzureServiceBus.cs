using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Service.Proxies
{
    public class AzureServiceBus
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }
    }
}
