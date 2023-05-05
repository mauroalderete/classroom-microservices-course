using System.Collections.Generic;
using static Api.Gateway.Models.Order.Common.Enums;

namespace Api.Gateway.Models.Order.Commands
{
    public class OrderCreateCommand
    {
        public OrderStatus Status { get; set; }
        public OrderPayment PaymentType { get; set; }
        public int ClientId { get; set; }
        public ICollection<OrderDetailCreateCommand> Items { get; set; } = new List<OrderDetailCreateCommand>();
    }
}
