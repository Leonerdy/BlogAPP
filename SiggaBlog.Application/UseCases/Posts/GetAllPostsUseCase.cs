using SiggaBlog.Domain.Entities;
using SiggaBlog.Domain.Interfaces;

namespace SiggaBlog.Application.UseCases.Posts
{
    public class GetAllPostsUseCase
    {
        private readonly IPostRepository _postRepository;

        public GetAllPostsUseCase(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> ExecuteAsync()
        {
            return await _postRepository.GetAllAsync();
        }
    }
}