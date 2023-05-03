using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static Catalog.common.Enums;

namespace Catalog.Service.EventHandlers.Commands
{
    public class ProductInStockUpdateCommand : INotification
    {
        public IEnumerable<ProductInStockUpdateItem> Items { get; set; } = new List<ProductInStockUpdateItem>();
    }

    public class ProductInStockUpdateItem : INotification
    {
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public ProductInStockAction Action { get; set; }
    }
}
