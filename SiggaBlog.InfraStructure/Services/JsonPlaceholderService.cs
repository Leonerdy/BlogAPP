using System.Net.Http.Json;
using SiggaBlog.Domain.Entities;

namespace SiggaBlog.InfraStructure.Services
{
    public class JsonPlaceholderService : IJsonPlaceholderService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://jsonplaceholder.typicode.com";

        public JsonPlaceholderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Post>>("posts") ?? Enumerable.Empty<Post>();
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            var response = await _httpClient.PostAsJsonAsync("posts", post);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Post>();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Comment>>($"posts/{postId}/comments") ?? Enumerable.Empty<Comment>();
        }
               
    }
} 