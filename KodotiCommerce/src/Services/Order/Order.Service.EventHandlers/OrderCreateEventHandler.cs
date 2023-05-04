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
using Microsoft.Extensions.Logging;

namespace Order.Service.EventHandlers
{
    public class OrderCreateEventHandler : INotificationHandler<OrderCreateCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public OrderCreateEventHandler(
            ApplicationDbContext dbContext,
            ILogger<OrderCreateEventHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task Handle(OrderCreateCommand notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("OrderCreateCommand started");
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
            _logger.LogInformation("OrderCreateCommand completed");
        }
    }
}
