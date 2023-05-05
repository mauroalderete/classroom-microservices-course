using Catalog.Listener.Commands;
using Catalog.Listener.Configurations;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Listener.Proxies
{
    // TODO: Catalog API requires jwt authentication
    public class CatalogHttpProxy : ICatalogProxy
    {
        private readonly ApiUrls _urls;
        private readonly HttpClient _httpClient;

        public CatalogHttpProxy(ApiUrls urls)
        {
            _urls = urls;
            _httpClient = new HttpClient();
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
