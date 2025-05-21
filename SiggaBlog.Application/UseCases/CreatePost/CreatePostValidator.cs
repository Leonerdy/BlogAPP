using FluentValidation;

namespace SiggaBlog.Application.UseCases.CreatePost
{
    public sealed class CreatePostValidator : AbstractValidator<CreatePostRequest>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(10);
            RuleFor(x => x.Body).NotEmpty().MinimumLength(10).MaximumLength(50);
        }
    }
}
