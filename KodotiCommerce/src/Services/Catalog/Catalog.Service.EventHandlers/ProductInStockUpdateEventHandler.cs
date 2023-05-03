using Catalog.Domain;
using Catalog.Persistence.Database;
using Catalog.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

        public ProductInStockUpdateStockEventHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(ProductInStockUpdateCommand notification, CancellationToken cancellationToken)
        {
            var products = notification.Items.Select(p => p.ProductId);
            var stocks = await _dbContext.Stocks.Where(x => products.Contains(x.ProductId)).ToListAsync();

            foreach( var item in notification.Items)
            {
                var entry = stocks.SingleOrDefault(x => x.ProductId == item.ProductId);

                if (item.Action == common.Enums.ProductInStockAction.Substract)
                {
                    if (entry == null || item.Stock > entry.Stock)
                    {
                        throw new Exception($"Product {entry.ProductId} - doesn't have enough stock");
                    }

                    entry.Stock -= item.Stock;
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
                    }

                    entry.Stock += item.Stock;
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
