using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using SiggaBlog.Domain.Entities;
using SiggaBlog.Domain.Interfaces;
using SiggaBlog.Application.UseCases.Posts;
using SiggaBlog.Tests.Integration;
using SiggaBlog.InfraStructure.Repositories;
using SiggaBlog.InfraStructure.Services;
using System.Net.Http;

namespace SiggaBlog.Tests.Integration.UseCases.Posts
{
    public class GetAllPostsUseCaseIntegrationTests : TestBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IGetAllPostsUseCase _useCase;
        private readonly INetworkStatus _networkStatus;
        private readonly HttpClient _httpClient;

        public GetAllPostsUseCaseIntegrationTests()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };
            
            _networkStatus = new MockNetworkStatus();
            _postRepository = new PostRepository(
                new JsonPlaceholderService(_httpClient),
                _dbContext,
                _networkStatus);
            _useCase = new GetAllPostsUseCase(_postRepository);
        }

        public override void Dispose()
        {
            _httpClient.Dispose();
            base.Dispose();
        }

        [Fact]
        public async Task ExecuteAsync_WhenOnline_ShouldFetchAndSavePosts()
        {
            // Arrange
            ((MockNetworkStatus)_networkStatus).SetHasInternetConnection(true);

            // Act
            var result = await _useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);

            // Verify posts were saved to database
            var savedPosts = await _dbContext.Posts.ToListAsync();
            Assert.Equal(result.Count(), savedPosts.Count);
        }

        [Fact]
        public async Task ExecuteAsync_WhenOffline_ShouldReturnLocalPosts()
        {
            // Arrange
            ((MockNetworkStatus)_networkStatus).SetHasInternetConnection(false);

            // Add some posts to local database
            var localPosts = new[]
            {
                new Post { Id = 1, UserId = 1, Title = "Local Post 1", Body = "Body 1" },
                new Post { Id = 2, UserId = 1, Title = "Local Post 2", Body = "Body 2" }
            };

            await _dbContext.Posts.AddRangeAsync(localPosts);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(localPosts.Length, result.Count());
            Assert.Equal(localPosts[0].Title, result.First().Title);
            Assert.Equal(localPosts[1].Title, result.Last().Title);
        }

        [Fact]
        public async Task ExecuteAsync_WhenOnlineAndOffline_ShouldHandleBothScenarios()
        {
            // Arrange
            var localPosts = new[]
            {
                new Post { Id = 1, UserId = 1, Title = "Local Post 1", Body = "Body 1" },
                new Post { Id = 2, UserId = 1, Title = "Local Post 2", Body = "Body 2" }
            };

            await _dbContext.Posts.AddRangeAsync(localPosts);
            await _dbContext.SaveChangesAsync();

            // Test online scenario
            ((MockNetworkStatus)_networkStatus).SetHasInternetConnection(true);
            var onlineResult = await _useCase.ExecuteAsync();
            Assert.NotNull(onlineResult);
            Assert.NotEmpty(onlineResult);

            // Test offline scenario
            ((MockNetworkStatus)_networkStatus).SetHasInternetConnection(false);
            var offlineResult = await _useCase.ExecuteAsync();
            Assert.NotNull(offlineResult);
            Assert.Equal(onlineResult.Count(), offlineResult.Count());
        }
    }

    internal class MockNetworkStatus : INetworkStatus
    {
        private bool _hasInternetConnection;

        public void SetHasInternetConnection(bool value)
        {
            _hasInternetConnection = value;
        }

        public bool HasInternetConnection()
        {
            return _hasInternetConnection;
        }
    }
} 