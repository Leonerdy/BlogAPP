using System.Net.Http.Json;
using SiggaBlog.Domain.Entities;
using SiggaBlog.Domain.Constants;

namespace SiggaBlog.InfraStructure.Services
{
    public class JsonPlaceholderService : IJsonPlaceholderService
    {
        private readonly HttpClient _httpClient;

        public JsonPlaceholderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(ApiConstants.JsonPlaceholderBaseUrl);
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Post>>("posts") ?? Enumerable.Empty<Post>();
            }
            
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro inesperado ao buscar posts: {ex.Message}");
                throw new Exception("Ocorreu um erro ao buscar os posts. Tente novamente mais tarde.");
            }
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("posts", post);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Post>();
            }
            
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro inesperado ao criar post: {ex.Message}");
                throw new Exception("Ocorreu um erro ao criar o post. Tente novamente mais tarde.");
            }
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Comment>>($"posts/{postId}/comments") ?? Enumerable.Empty<Comment>();
            }
            
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro inesperado ao buscar comentários: {ex.Message}");
                throw new Exception("Ocorreu um erro ao buscar os comentários. Tente novamente mais tarde.");
            }
        }
    }
} 