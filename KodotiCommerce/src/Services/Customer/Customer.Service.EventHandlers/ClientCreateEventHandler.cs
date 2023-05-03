using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MediatR;
using Customer.Domain;
using Customer.Persistence.Database;
using Customer.Service.EventHandlers.Commands;

namespace Customer.Service.EventHandlers
{
    public class ClientCreateEventHandler : INotificationHandler<ClientCreateCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public ClientCreateEventHandler(ApplicationDbContext dbContext, ILogger<ClientCreateEventHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        
        public async Task Handle(ClientCreateCommand notification, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(
                new Client
                {
                    Name = notification.Name,
                });
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
