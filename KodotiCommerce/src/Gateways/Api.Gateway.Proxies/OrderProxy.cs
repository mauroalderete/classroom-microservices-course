using Api.Gateway.Models.Collection;
using Api.Gateway.Models.Order.Commands;
using Api.Gateway.Models.Order.DTO;
using Api.Gateway.Proxies.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies
{
    public interface IOrderProxy
    {
        Task<DataCollection<OrderDto>> GetOrdersAsync();
        Task<OrderDto> GetOrderAsync(int id);
        Task CreateOrderAsync(OrderCreateCommand command);
    }

    public class OrderProxy : IOrderProxy
    {
        private readonly ApiUrls _urls;
        private readonly HttpClient _httpClient;

        public OrderProxy(IOptions<ApiUrls> urls, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _urls = urls.Value;
            _httpClient = httpClient;

            _httpClient.AddBearerToken(httpContextAccessor);
        }

        public async Task<DataCollection<OrderDto>> GetOrdersAsync()
        {
            var request = await _httpClient.GetAsync($"{_urls.Order}/v1/orders");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<OrderDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task<OrderDto> GetOrderAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_urls.Order}/v1/orders/{id}");
            request.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<OrderDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task CreateOrderAsync(OrderCreateCommand command)
        {
            var content = new StringContent(
                               JsonSerializer.Serialize(command), encoding: System.Text.Encoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync($"{_urls.Order}/v1/orders", content);
            request.EnsureSuccessStatusCode();
        }
        
    }
}
