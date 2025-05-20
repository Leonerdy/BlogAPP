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
            if (!_networkStatus.IsOnline)
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
                .OrderByDescending(c => c.Id)
                .ToListAsync();
        }

        private async Task UpdateLocalDatabaseAsync(IEnumerable<Comment> comments)
        {
            try
            {
                await _dbContext.Database.EnsureCreatedAsync();

                var postId = comments.FirstOrDefault()?.PostId ?? 0;
                if (postId > 0)
                {
                    var existingComments = await _dbContext.Comments
                        .Where(c => c.PostId == postId)
                        .ToListAsync();

                    if (existingComments.Any())
                    {
                        _dbContext.Comments.RemoveRange(existingComments);
                    }
                }

                await _dbContext.Comments.AddRangeAsync(comments);
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