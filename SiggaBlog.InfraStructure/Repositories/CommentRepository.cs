using Microsoft.EntityFrameworkCore;
using SiggaBlog.Domain.Entities;
using SiggaBlog.Domain.Interfaces;
using SiggaBlog.InfraStructure.Persistence;
using SiggaBlog.InfraStructure.Services;

namespace SiggaBlog.InfraStructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IJsonPlaceholderService _jsonPlaceholderService;
        private readonly SiggaBlogDbContext _dbContext;
        private readonly INetworkStatus _networkStatus;

        public CommentRepository(
            IJsonPlaceholderService jsonPlaceholderService,
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
                return await _jsonPlaceholderService.GetCommentsByPostIdAsync(postId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro buscando comentários: {ex.Message}");
                return Enumerable.Empty<Comment>();
            }
        }

        private async Task<IEnumerable<Comment>> GetLocalCommentsAsync(int postId)
        {
            return await _dbContext.Comments
                .Where(c => c.PostId == postId)
                .AsNoTracking()
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
                System.Diagnostics.Debug.WriteLine($"Erro atualizando o banco de dados: {ex.Message}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
        }
                
    }
} 