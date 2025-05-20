using SiggaBlog.Domain.Entities;

namespace SiggaBlog.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetByPostIdAsync(int postId);
    }
} 