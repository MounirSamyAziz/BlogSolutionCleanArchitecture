using AutoMapper;
using BlogSolutionClean.Domain.Entities.Entities;
using BlogSolutionClean.Infrastructure.Interfaces;
using BlogSolutionClean.Application.Dtos;
using FluentValidation;
using BlogSolutionClean.Application.Contracts.Interfaces;

namespace BlogSolutionClean.Application.Services;

/// <summary>
/// Service for managing authors.
/// </summary>
public class AuthorService : IAuthorService
{
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;
    private readonly IValidator<AuthorDto> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorService"/> class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="authorRepository">The author repository instance.</param>
    public AuthorService(IMapper mapper, IAuthorRepository authorRepository, IValidator<AuthorDto> validator)
    {
        _mapper = mapper;
        _authorRepository = authorRepository;
        _validator = validator;
    }

    /// <summary>
    /// Creates a new author.
    /// </summary>
    /// <param name="authorDto">The author data to create.</param>
    /// <returns>The created author DTO.</returns>
    public async Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto)
    {
        // Validate the postDto using FluentValidation
        var validationResult = await _validator.ValidateAsync(authorDto);

        if (!validationResult.IsValid)
        {
            // Handle validation errors (e.g., throw an exception or return error details)
            throw new ValidationException(validationResult.Errors);
        }
        var author = _mapper.Map<Author>(authorDto);
        var createdAuthor = await _authorRepository.CreateAuthorAsync(author);
        return _mapper.Map<AuthorDto>(createdAuthor);
    }
}

