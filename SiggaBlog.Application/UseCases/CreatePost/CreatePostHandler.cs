using AutoMapper;
using MediatR;
using SiggaBlog.Domain.Entities;
using SiggaBlog.Domain.Interfaces;

namespace SiggaBlog.Application.UseCases.CreatePost
{
    public class CreatePostHandler : IRequestHandler<CreatePostRequest, CreatePostResponse>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public CreatePostHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<CreatePostResponse> Handle(CreatePostRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var post = _mapper.Map<Post>(request);
                var createdPost = await _postRepository.CreatePostAsync(post);
                return _mapper.Map<CreatePostResponse>(createdPost);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in CreatePostHandler: {ex.Message}");
                throw;
            }
        }
    }
}
