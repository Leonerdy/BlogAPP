using SiggaBlog.Domain.Entities;

namespace SiggaBlog.InfraStructure.Services
{
    public interface IJsonPlaceholderService
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> CreatePostAsync(Post post);
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
       
    }
} 