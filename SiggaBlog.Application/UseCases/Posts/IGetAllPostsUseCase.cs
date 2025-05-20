using SiggaBlog.Domain.Entities;

namespace SiggaBlog.Application.UseCases.Posts
{
    public interface IGetAllPostsUseCase
    {
        Task<IEnumerable<Post>> ExecuteAsync();
    }
} 