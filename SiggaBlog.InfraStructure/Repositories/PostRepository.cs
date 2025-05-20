using SiggaBlog.Domain.Entities;
using SiggaBlog.Domain.Interfaces;
using SiggaBlog.InfraStructure.Services;

namespace SiggaBlog.InfraStructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly JsonPlaceholderService _jsonPlaceholderService;

        public PostRepository(JsonPlaceholderService jsonPlaceholderService)
        {
            _jsonPlaceholderService = jsonPlaceholderService;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _jsonPlaceholderService.GetAllAsync<Post>("posts");
        }
    }
} 