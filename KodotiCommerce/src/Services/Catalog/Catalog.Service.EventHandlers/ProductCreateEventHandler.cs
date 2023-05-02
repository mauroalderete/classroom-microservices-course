using Catalog.Domain;
using Catalog.Persistence.Database;
using Catalog.Service.EventHandlers.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Service.EventHandlers
{
    public class ProductCreateEventHandler : INotificationHandler<ProductCreateCommand>
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductCreateEventHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(ProductCreateCommand notification, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(
                new Product
                {
                    Name = notification.Name,
                    Description = notification.Description,
                    Price = notification.Price
                });

            await _dbContext.SaveChangesAsync();

        }
    }
}
