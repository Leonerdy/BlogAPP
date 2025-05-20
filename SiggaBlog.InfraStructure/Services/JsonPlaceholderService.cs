using System.Net.Http.Json;

namespace SiggaBlog.InfraStructure.Services
{
    public class JsonPlaceholderService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://jsonplaceholder.typicode.com";

        public JsonPlaceholderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string endpoint)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<T>>(endpoint) ?? Enumerable.Empty<T>();
        }
    }
} 