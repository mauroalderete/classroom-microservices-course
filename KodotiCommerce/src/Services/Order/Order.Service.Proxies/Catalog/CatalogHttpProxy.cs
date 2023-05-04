using Microsoft.Extensions.Options;
using Order.Service.Proxies.Catalog.Commands;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace Order.Service.Proxies.Catalog
{
    public class CatalogHttpProxy : ICatalogProxy
    {
        private readonly ApiUrls _urls;
        private readonly HttpClient _httpClient;

        public CatalogHttpProxy(IOptions<ApiUrls> urls, HttpClient httpClient)
        {
            _urls = urls.Value;
            _httpClient = httpClient;
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
