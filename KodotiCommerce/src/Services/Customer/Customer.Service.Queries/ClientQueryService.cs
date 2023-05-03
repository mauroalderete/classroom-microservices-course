using System;
using Service.Common.Collection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Service.Common.Mapping;
using Service.Common.Paging;
using Customer.Persistence.Database;
using Customer.Service.Queries.DTO;

namespace Customer.Service.Queries
{
    public interface IClientQueryService
    {
        Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);
        Task<ClientDto> GetAsync(int id);
    }
    public class ClientQueryService : IClientQueryService
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientQueryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null)
        {
            var collection = await _dbContext.Clients.Where(c => clients == null || clients.Contains(c.ClientId))
                .OrderByDescending(c => c.ClientId)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<ClientDto>>();
        }

        public async Task<ClientDto> GetAsync(int id)
        {
            var t = await _dbContext.Clients.SingleAsync(c => c.ClientId == id);

            return (await _dbContext.Clients.SingleAsync(c => c.ClientId == id)).MapTo<ClientDto>();
        }
    }
}
