namespace Api.Gateway.Models.Order.Commands
{
    public class OrderDetailCreateCommand
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
