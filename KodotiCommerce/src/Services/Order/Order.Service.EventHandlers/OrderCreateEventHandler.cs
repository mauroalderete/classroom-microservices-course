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
using Order.Service.Proxies.Catalog;
using Order.Service.Proxies.Catalog.Commands;

namespace Order.Service.EventHandlers
{
    public class OrderCreateEventHandler : INotificationHandler<OrderCreateCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly ICatalogProxy _catalogProxy;

        public OrderCreateEventHandler(
            ApplicationDbContext dbContext,
            ILogger<OrderCreateEventHandler> logger,
            ICatalogProxy catalogProxy)
        {
            _dbContext = dbContext;
            _logger = logger;
            _catalogProxy = catalogProxy;
        }

        public async Task Handle(OrderCreateCommand notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("OrderCreateCommand started");

            var order = new Domain.Order();

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                _logger.LogInformation("Preparing order detail");
                PrepareOrderDetail(order, notification);

                _logger.LogInformation("Preparing order header");
                PrepareOrder(order, notification);

                _logger.LogInformation("Saving order");
                await _dbContext.AddAsync(order);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Order ${order.OrderId} was saved");

                //TODO: Update Stock notification
                _logger.LogInformation("Updating stock");
                await _catalogProxy.UpdateStockAsync(new ProductInStockUpdateCommand
                {
                    Items = notification.Items.Select(i => new ProductInStockUpdateItem
                    {
                        Action = ProductInStockAction.Substract,
                        ProductId = i.ProductId,
                        Stock = i.Quantity,
                    }),
                });

                await transaction.CommitAsync();
            }
            _logger.LogInformation("OrderCreateCommand completed");
        }

        private void PrepareOrderDetail(Domain.Order order, OrderCreateCommand notification)
        {
            order.Items = notification.Items.Select(x => new OrderDetail()
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                Total = x.Quantity * x.UnitPrice
            }).ToList();
        }

        private void PrepareOrder(Domain.Order order, OrderCreateCommand notification)
        {
            order.ClientId = notification.ClientId;
            order.CreatedAt = DateTime.Now;
            order.Status = notification.Status;
            order.PaymentType = notification.PaymentType;

            order.Total = order.Items.Sum(x => x.Total);
        }
    }
}
