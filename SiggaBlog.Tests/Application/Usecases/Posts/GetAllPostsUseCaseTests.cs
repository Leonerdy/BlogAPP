using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using SiggaBlog.Domain.Entities;
using SiggaBlog.Domain.Interfaces;
using SiggaBlog.Application.UseCases.Posts;

namespace SiggaBlog.Tests.Application.Usecases.Posts
{
    public class GetAllPostsUseCaseTests
    {
        private readonly Mock<IPostRepository> _postRepositoryMock;
        private readonly GetAllPostsUseCase _useCase;

        public GetAllPostsUseCaseTests()
        {
            _postRepositoryMock = new Mock<IPostRepository>();
            _useCase = new GetAllPostsUseCase(_postRepositoryMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_WhenPostsExist_ShouldReturnAllPosts()
        {
            // Arrange
            var expectedPosts = new List<Post>
            {
                new Post(1, "Test Post 1", "Body 1"),
                new Post(2, "Test Post 2", "Body 2")
            };

            _postRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(expectedPosts);

            // Act
            var result = await _useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedPosts.Count, result.Count());
            Assert.Equal(expectedPosts[0].Title, result.First().Title);
            Assert.Equal(expectedPosts[1].Title, result.Last().Title);
        }

        [Fact]
        public async Task ExecuteAsync_WhenNoPostsExist_ShouldReturnEmptyList()
        {
            // Arrange
            _postRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Post>());

            // Act
            var result = await _useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task ExecuteAsync_WhenRepositoryThrowsException_ShouldPropagateException()
        {
            // Arrange
            _postRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ThrowsAsync(new Exception("Test exception"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _useCase.ExecuteAsync());
        }
    }
}
