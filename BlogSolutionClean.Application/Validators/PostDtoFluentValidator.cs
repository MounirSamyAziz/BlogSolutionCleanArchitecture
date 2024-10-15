using BlogSolutionClean.Shared.Dtos;
using FluentValidation;

namespace BlogSolutionClean.Application.Validators
{
    public class PostDtoFluentValidator : AbstractValidator<PostDto>
    {
        public PostDtoFluentValidator()
        {
            RuleFor(post => post.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(3, 100).WithMessage("Title must be between 3 and 100 characters.");

            RuleFor(post => post.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(post => post.Content)
                .NotEmpty().WithMessage("Content is required.");

            RuleFor(post => post.AuthorName)
                .NotEmpty().WithMessage("Author name is required.");

            RuleFor(post => post.AuthorSurname)
                .NotEmpty().WithMessage("Author surname is required.");
        }
    }

}


