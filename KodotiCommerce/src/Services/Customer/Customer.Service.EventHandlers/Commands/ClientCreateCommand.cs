using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Service.EventHandlers.Commands
{
    public class ClientCreateCommand : INotification
    {
        public string Name { get; set; }
    }
}
