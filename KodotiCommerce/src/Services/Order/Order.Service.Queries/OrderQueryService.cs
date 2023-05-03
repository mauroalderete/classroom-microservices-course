using System;
using Service.Common.Collection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Service.Common.Mapping;
using Service.Common.Paging;
using Order.Service.Queries.DTO;
using Order.Persistence.Database;

namespace Customer.Service.Queries
{
    public interface IOrderQueryService
    {
        Task<DataCollection<OrderDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);
        Task<OrderDto> GetAsync(int id);
    }
    public class OrderQueryService : IOrderQueryService
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderQueryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DataCollection<OrderDto>> GetAllAsync(int page, int take, IEnumerable<int> orders = null)
        {
            var collection = await _dbContext.Orders.Where(o => orders == null || orders.Contains(o.OrderId))
                .OrderByDescending(c => c.OrderId)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<OrderDto>>();
        }

        public async Task<OrderDto> GetAsync(int id)
        {
            var t = await _dbContext.Orders.SingleAsync(o => o.OrderId == id);

            return (await _dbContext.Orders.SingleAsync(o => o.OrderId == id)).MapTo<OrderDto>();
        }
    }
}
