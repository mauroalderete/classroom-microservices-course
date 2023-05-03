using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain;

namespace Order.Persistence.Database.Configuration
{
    public class OrderDetailConfiguration
    {
        public OrderDetailConfiguration(EntityTypeBuilder<OrderDetail> entityBuilder)
        {
            entityBuilder.HasIndex(o => o.OrderDetailId);

            var details = new List<OrderDetail>();

            for (var i = 1; i <= 20; i++)
            {
                details.Add(new OrderDetail
                {
                    OrderDetailId = i,
                    ProductId = i,
                    Quantity = i,
                    UnitPrice = i,
                    Total = i * 1
                });
            }

            entityBuilder.HasData(details);
        }
    }
}
