using System;

namespace Catalog.Service.Queries.DTO
{
    public class ProductInStockDto
    {
        public int ProductInStockId { get; set; }
        public int ProductId { get; set; }
        public int Stock { get; set; }
    }
}
