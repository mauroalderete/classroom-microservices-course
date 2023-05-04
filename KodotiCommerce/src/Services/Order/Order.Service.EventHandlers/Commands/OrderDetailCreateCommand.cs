using MediatR;
using Order.Domain;
using System;
using System.Collections.Generic;
using static Order.Common.Enums;

namespace Order.Service.EventHandlers.Commands
{
    public class OrderDetailCreateCommand : INotification
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
