using SiggaBlog.Domain.Entities;

namespace SiggaBlog.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
    }
} 