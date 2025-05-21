using MediatR;

namespace SiggaBlog.Application.UseCases.CreatePost
{
    public record CreatePostRequest(
        string Title,
        string Body,
        int UserId
    ) : IRequest<CreatePostResponse>;
   
    
}
