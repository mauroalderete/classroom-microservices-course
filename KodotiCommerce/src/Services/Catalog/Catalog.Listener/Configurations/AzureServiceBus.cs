using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Listener.Configurations
{
    public class AzureServiceBus
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }
    }
}
