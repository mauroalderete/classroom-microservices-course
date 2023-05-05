using System.Collections.Generic;
using static Api.Gateway.Models.Catalog.Common.Enums;

namespace Api.Gateway.Models.Catalog.Commands
{
    public class ProductInStockUpdateCommand
    {
        public IEnumerable<ProductInStockUpdateItem> Items { get; set; } = new List<ProductInStockUpdateItem>();
    }

    public class ProductInStockUpdateItem
    {
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public ProductInStockAction Action { get; set; }
    }
}
