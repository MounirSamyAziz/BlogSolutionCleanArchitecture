using BlogSolutionClean.Application.Dtos;
using FluentValidation;

namespace BlogSolutionClean.Application.Validators;

public class AuthorDtoFluentValidator : AbstractValidator<AuthorDto>
{
    public AuthorDtoFluentValidator()
    {
        RuleFor(post => post.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 100).WithMessage("Title must be between 3 and 100 characters.");

        RuleFor(post => post.Surname)
            .NotEmpty().WithMessage("Surname is required.")
            .Length(3, 100).WithMessage("Title must be between 3 and 100 characters."); 
    }
}


