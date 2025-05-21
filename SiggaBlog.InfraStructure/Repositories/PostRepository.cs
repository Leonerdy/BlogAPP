using SiggaBlog.Domain.Entities;
using SiggaBlog.Domain.Interfaces;
using SiggaBlog.InfraStructure.Services;
using SiggaBlog.InfraStructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace SiggaBlog.InfraStructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly JsonPlaceholderService _jsonPlaceholderService;
        private readonly SiggaBlogDbContext _dbContext;
        private readonly INetworkStatus _networkStatus;

        public PostRepository(
            JsonPlaceholderService jsonPlaceholderService,
            SiggaBlogDbContext dbContext,
            INetworkStatus networkStatus)
        {
            _jsonPlaceholderService = jsonPlaceholderService;
            _dbContext = dbContext;
            _networkStatus = networkStatus;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            if (!_networkStatus.HasInternetConnection())
            {
                return await GetLocalPostsAsync();
            }

            var remotePosts = await GetRemotePostsAsync();
            if (remotePosts.Any())
            {
                await UpdateLocalDatabaseAsync(remotePosts);
                return remotePosts;
            }

            return await GetLocalPostsAsync();
        }

        private async Task<IEnumerable<Post>> GetRemotePostsAsync()
        {
            try
            {
                return await _jsonPlaceholderService.GetAllAsync<Post>("posts");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching remote posts: {ex.Message}");
                return Enumerable.Empty<Post>();
            }
        }

        private async Task<IEnumerable<Post>> GetLocalPostsAsync()
        {
            return await _dbContext.Posts
                .AsNoTracking()
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        private async Task UpdateLocalDatabaseAsync(IEnumerable<Post> remotePosts)
        {
            try
            {
                await _dbContext.Database.EnsureCreatedAsync();

                foreach (var post in remotePosts)
                {
                    bool exists = await _dbContext.Posts.AnyAsync(p => p.Id == post.Id);
                    if (!exists)
                    {
                        await _dbContext.Posts.AddAsync(post);
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