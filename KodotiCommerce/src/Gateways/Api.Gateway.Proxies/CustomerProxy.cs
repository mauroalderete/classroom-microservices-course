using Api.Gateway.Models.Collection;
using Api.Gateway.Models.Customer.Commands;
using Api.Gateway.Models.Customer.DTO;
using Api.Gateway.Proxies.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies
{
    public class CustomerProxy
    {
        private readonly ApiUrls _urls;
        private readonly HttpClient _httpClient;

        public CustomerProxy(IOptions<ApiUrls> urls, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _urls = urls.Value;
            _httpClient = httpClient;

            _httpClient.AddBearerToken(httpContextAccessor);
        }
        public async Task<DataCollection<ClientDto>> GetAll(
            int page = 1,
            int take = 10,
            string ids = null)
        {
            var request = await _httpClient.GetAsync($"{_urls.Customer}/v1/clients?page=${page}&take=${take}&ids=${ids}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<ClientDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task<ClientDto> Get(int id)
        {
            var request = await _httpClient.GetAsync($"{_urls.Customer}/v1/clients/${id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ClientDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task Create(ClientCreateCommand command)
        {
            var content = new StringContent(
                               JsonSerializer.Serialize(command), encoding: System.Text.Encoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync($"{_urls.Customer}/v1/clients", content);
            request.EnsureSuccessStatusCode();
        }
    }
}
