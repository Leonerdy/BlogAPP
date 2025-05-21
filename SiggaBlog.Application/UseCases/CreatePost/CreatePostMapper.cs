using AutoMapper;
using SiggaBlog.Domain.Entities;

namespace SiggaBlog.Application.UseCases.CreatePost
{
    public sealed class CreatePostMapper : Profile
    {
        public CreatePostMapper()
        {
            CreateMap<CreatePostRequest, Post>();
            CreateMap<Post, CreatePostResponse>();
        }
    }
}
