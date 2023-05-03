using Catalog.Domain;
using Catalog.Persistence.Database;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.EventHandlers.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Service.EventHandlers
{
    public class ProductInStockUpdateStockEventHandler : INotificationHandler<ProductInStockUpdateCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public ProductInStockUpdateStockEventHandler(ApplicationDbContext dbContext, ILogger<ProductInStockUpdateStockEventHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task Handle(ProductInStockUpdateCommand notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--- ProductInStockUpdateCommand started");

            var products = notification.Items.Select(p => p.ProductId);
            var stocks = await _dbContext.Stocks.Where(x => products.Contains(x.ProductId)).ToListAsync();

            _logger.LogInformation("--- Retrieve products from database");

            foreach( var item in notification.Items)
            {
                var entry = stocks.SingleOrDefault(x => x.ProductId == item.ProductId);

                if (item.Action == common.Enums.ProductInStockAction.Substract)
                {
                    if (entry == null || item.Stock > entry.Stock)
                    {
                        _logger.LogError($"Product {entry.ProductId} - doesn't have enough stock");
                        throw new ProductInStockUpdateCommandException($"Product {entry.ProductId} - doesn't have enough stock");
                    }

                    entry.Stock -= item.Stock;
                    _logger.LogInformation($"--- Product {entry.ProductId} - its stock was subtracted - new stock {entry.Stock}");
                }
                else
                {
                    
                    if (entry == null )
                    {
                        entry = new ProductInStock
                        {
                            ProductId = item.ProductId,
                            Stock = 0
                        };
                        await _dbContext.AddAsync(entry);

                        _logger.LogInformation($"--- New stock record was created for {entry.ProductId}");
                    }

                    entry.Stock += item.Stock;
                    _logger.LogInformation($"--- Product {entry.ProductId} - its stock was incremented - new stock {entry.Stock}");
                }
            }

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("--- ProductInStockUpdateCommand finished");
        }
    }
}
