using Api.Gateway.Models.Catalog.Commands;
using Api.Gateway.Models.Catalog.DTO;
using Api.Gateway.Models.Collection;
using Api.Gateway.Proxies.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies
{
    public interface ICatalogProxy
    {
        Task<DataCollection<ProductDto>> GetAll(int page = 1, int take = 10, string ids = null);
        Task<ProductDto> Get(int id);
        Task Create(ProductCreateCommand command);
        Task UpdateStockAsync(ProductInStockUpdateCommand command);
    }

    public class CatalogProxy : ICatalogProxy
    {
        private readonly ApiUrls _urls;
        private readonly HttpClient _httpClient;

        public CatalogProxy(IOptions<ApiUrls> urls, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _urls = urls.Value;
            _httpClient = httpClient;

            _httpClient.AddBearerToken(httpContextAccessor);
        }

        public async Task<DataCollection<ProductDto>> GetAll(
            int page = 1,
            int take = 10,
            string ids = null)
        {
            var request = await _httpClient.GetAsync($"{_urls.Catalog}/v1/products?page={page}&take={take}&ids={ids}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<ProductDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task<ProductDto> Get(int id)
        {
            var request = await _httpClient.GetAsync($"{_urls.Catalog}/v1/products/{id}");
            request.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<ProductDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task Create(ProductCreateCommand command)
        {
            var content = new StringContent(
                               JsonSerializer.Serialize(command), encoding: System.Text.Encoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync($"{_urls.Catalog}/v1/products", content);
            request.EnsureSuccessStatusCode();
        }

        public async Task UpdateStockAsync(ProductInStockUpdateCommand command)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(command), encoding: System.Text.Encoding.UTF8, "application/json");

            var request = await _httpClient.PutAsync($"{_urls.Catalog}/v1/stocks", content);
            request.EnsureSuccessStatusCode();
        }
    }
}
