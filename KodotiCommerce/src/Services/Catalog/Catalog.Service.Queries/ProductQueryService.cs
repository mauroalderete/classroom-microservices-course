using Catalog.Persistence.Database;
using Catalog.Service.Queries.DTO;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Catalog.Service.Queries
{
    public interface IProductQueryService {
        Task<DataCollection<ProductDto>> GetAllAsync(int page, int take, IEnumerable<int> products = null);
        Task<ProductDto> GetAsync(int d);
    }
    public class ProductQueryService : IProductQueryService
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductQueryService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<DataCollection<ProductDto>> GetAllAsync(int page, int take, IEnumerable<int> products = null)
        {
            var collection = await _dbContext.Products.Where(p=> products == null || products.Contains(p.ProductId))
                .OrderByDescending(p => p.ProductId)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<ProductDto>>();
        }

        public async Task<ProductDto> GetAsync(int d)
        {
            var t = await _dbContext.Products.SingleAsync(p => p.ProductId == d);

            return (await _dbContext.Products.SingleAsync(p => p.ProductId == d)).MapTo<ProductDto>();
        }
    }
}
