using Catalog.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Persistence.Database.Configuration
{
    public class ProductInStockConfiguration
    {
        public ProductInStockConfiguration(EntityTypeBuilder<ProductInStock> entityBuilder) {
            entityBuilder.HasIndex(p => p.ProductInStockId);

            var random = new Random();

            var productsInStock = new List<ProductInStock>();

            for (var i = 1; i <= 100; i++)
            {
                productsInStock.Add(new ProductInStock
                {
                    ProductInStockId = i,
                    ProductId = i,
                    Stock = random.Next(0, 100)
                });
            }

            entityBuilder.HasData(productsInStock);
        }
    }
}
