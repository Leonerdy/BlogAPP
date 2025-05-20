using SiggaBlog.Domain.Entities;
using SiggaBlog.Domain.Interfaces;

namespace SiggaBlog.Application.UseCases.Comments
{
    public class GetCommentsByPostIdUseCase : IGetCommentsByPostIdUseCase
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentsByPostIdUseCase(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment>> ExecuteAsync(int postId)
        {
            return await _commentRepository.GetByPostIdAsync(postId);
        }
    }
} 