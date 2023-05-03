using MediatR;
using System.Collections.Generic;
using static Order.Common.Enums;

namespace Order.Service.EventHandlers.Commands
{
    public class OrderCreateCommand : INotification
    {
        public OrderStatus Status { get; set; }
        public OrderPayment PaymentType { get; set; }
        public int ClientId { get; set; }
        public ICollection<OrderDetailCreateCommand> Items { get; set; } = new List<OrderDetailCreateCommand>();
    }
}
