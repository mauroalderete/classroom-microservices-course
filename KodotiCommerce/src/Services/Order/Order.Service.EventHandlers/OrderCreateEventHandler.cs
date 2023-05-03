using Order.Domain;
using Order.Persistence.Database;
using Order.Service.EventHandlers.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Order.Service.EventHandlers
{
    public class OrderCreateEventHandler : INotificationHandler<OrderCreateCommand>
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderCreateEventHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(OrderCreateCommand notification, CancellationToken cancellationToken)
        {
            var order = new Domain.Order()
            {
                ClientId = notification.ClientId,
                CreatedAt = DateTime.Now,
                Status = notification.Status,
                PaymentType = notification.PaymentType,
            };

            foreach( var item in notification.Items)
            {
                order.Items.Add(new OrderDetail()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Total = item.Quantity * item.UnitPrice
                });
            }

            order.Total = order.Items.Sum(x => x.Total);

            await _dbContext.AddAsync(order);

            await _dbContext.SaveChangesAsync();
        }
    }
}
