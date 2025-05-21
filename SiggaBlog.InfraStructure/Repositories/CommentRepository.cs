using Microsoft.EntityFrameworkCore;
using SiggaBlog.Domain.Entities;
using SiggaBlog.Domain.Interfaces;
using SiggaBlog.InfraStructure.Persistence;
using SiggaBlog.InfraStructure.Services;

namespace SiggaBlog.InfraStructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly JsonPlaceholderService _jsonPlaceholderService;
        private readonly SiggaBlogDbContext _dbContext;
        private readonly INetworkStatus _networkStatus;

        public CommentRepository(
            JsonPlaceholderService jsonPlaceholderService,
            SiggaBlogDbContext dbContext,
            INetworkStatus networkStatus)
        {
            _jsonPlaceholderService = jsonPlaceholderService;
            _dbContext = dbContext;
            _networkStatus = networkStatus;
        }

        public async Task<IEnumerable<Comment>> GetByPostIdAsync(int postId)
        {
            if (!_networkStatus.HasInternetConnection())
            {
                return await GetLocalCommentsAsync(postId);
            }

            var remoteComments = await GetRemoteCommentsAsync(postId);
            if (remoteComments.Any())
            {
                await UpdateLocalDatabaseAsync(remoteComments);
                return remoteComments;
            }

            return await GetLocalCommentsAsync(postId);
        }

        private async Task<IEnumerable<Comment>> GetRemoteCommentsAsync(int postId)
        {
            try
            {
                return await _jsonPlaceholderService.GetAllAsync<Comment>($"posts/{postId}/comments");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching remote comments: {ex.Message}");
                return Enumerable.Empty<Comment>();
            }
        }

        private async Task<IEnumerable<Comment>> GetLocalCommentsAsync(int postId)
        {
            return await _dbContext.Comments
                .AsNoTracking()
                .Where(c => c.PostId == postId)
                .ToListAsync();
        }

        private async Task UpdateLocalDatabaseAsync(IEnumerable<Comment> remoteComments)
        {
            try
            {
                await _dbContext.Database.EnsureCreatedAsync();

                foreach (var comment in remoteComments)
                {
                    bool exists = await _dbContext.Comments.AnyAsync(c => c.Id == comment.Id);
                    if (!exists)
                    {
                        await _dbContext.Comments.AddAsync(comment);
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating local database: {ex.Message}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
        }
    }
} 