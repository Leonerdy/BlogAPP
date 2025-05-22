using SiggaBlog.Domain.Entities;
using SiggaBlog.Domain.Interfaces;
using SiggaBlog.InfraStructure.Services;
using SiggaBlog.InfraStructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace SiggaBlog.InfraStructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IJsonPlaceholderService _jsonPlaceholderService;
        private readonly SiggaBlogDbContext _dbContext;
        private readonly INetworkStatus _networkStatus;

        public PostRepository(
            IJsonPlaceholderService jsonPlaceholderService,
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
                return await _jsonPlaceholderService.GetAllPostsAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro buscando posts: {ex.Message}");
                return Enumerable.Empty<Post>();
            }
        }

        private async Task<IEnumerable<Post>> GetLocalPostsAsync()
        {
            return await _dbContext.Posts
                .AsNoTracking()
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
                System.Diagnostics.Debug.WriteLine($"Erro atualizando o banco de dados: {ex.Message}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            if (!_networkStatus.HasInternetConnection())
            {
                throw new InvalidOperationException("Não é possível criar um post sem conexão com a internet");
            }

            try
            {
                var createdPost = await _jsonPlaceholderService.CreatePostAsync(post);
                if (createdPost != null)
                {
                    // Verifica se já existe um post com ID 101
                    var existingPost = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == 101);
                    if (existingPost != null)
                    {
                        _dbContext.Posts.Remove(existingPost);
                        await _dbContext.SaveChangesAsync();
                    }

                    await _dbContext.Posts.AddAsync(createdPost);
                    await _dbContext.SaveChangesAsync();
                }
                return createdPost;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro criando Post: {ex.Message}");
                throw;
            }
        }
    }
} 