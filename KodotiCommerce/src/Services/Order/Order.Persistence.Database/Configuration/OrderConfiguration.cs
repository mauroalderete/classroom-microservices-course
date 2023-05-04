using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain;

namespace Order.Persistence.Database.Configuration
{
    public class OrderConfiguration
    {
        public OrderConfiguration(EntityTypeBuilder<Domain.Order> entityBuilder)
        {
            entityBuilder.HasIndex(o => o.OrderId);

            var orders = new List<Domain.Order>();

            for (var i = 1; i <= 20; i++)
            {
                orders.Add(new Domain.Order
                {
                    OrderId = i,
                    ClientId = i,
                    Status = Common.Enums.OrderStatus.Pending,
                    PaymentType = Common.Enums.OrderPayment.CreditCard,
                    CreatedAt = DateTime.Now,
                    Total = 100
                });
            }

            entityBuilder.HasData(orders);
        }
    }
}
