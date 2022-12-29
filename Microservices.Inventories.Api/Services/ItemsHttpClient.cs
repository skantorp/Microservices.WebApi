using System.Text.Json;
using Microservices.Common.DTOs;
using Microservices.Inventories.Api.Services.Interfaces;

namespace Microservices.Inventories.Api.Services
{
    public class ItemsHttpClient : IItemsHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;
        public ItemsHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<List<ItemDTO>> GetAllItems()
        {
            var httpClient = _httpClientFactory.CreateClient("ItemsClient");
            using (var response = await httpClient.GetAsync("items"))
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                var items = await JsonSerializer.DeserializeAsync<List<ItemDTO>>(stream, _options);

                return items;
            }
        }
    }
}
