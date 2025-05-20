using SiggaBlog.Domain.Entities;

namespace SiggaBlog.Application.UseCases.Comments
{
    public interface IGetCommentsByPostIdUseCase
    {
        Task<IEnumerable<Comment>> ExecuteAsync(int postId);
    }
} 