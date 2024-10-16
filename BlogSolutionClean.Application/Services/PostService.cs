using AutoMapper;
using BlogSolutionClean.Domain.Entities.Entities;
using BlogSolutionClean.Infrastructure.Interfaces;
using BlogSolutionClean.Application.Dtos;
using FluentValidation;
using BlogSolutionClean.Application.Contracts.Interfaces;

namespace BlogSolutionClean.Application.Services;

/// <summary>
/// Service for managing blog posts.
/// </summary>
public class PostService : IPostService
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IValidator<PostDto> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="PostService"/> class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="postRepository">The post repository instance.</param>
    /// <param name="authorRepository">The author repository instance.</param>
    public PostService(IMapper mapper, IPostRepository postRepository, IAuthorRepository authorRepository, IValidator<PostDto> validator)
    {
        _mapper = mapper;
        _postRepository = postRepository;
        _authorRepository = authorRepository;
        _validator = validator; 
    }

    /// <summary>
    /// Creates a new blog post along with the author information.
    /// </summary>
    /// <param name="postDto">The post data to create, including author information.</param>
    /// <returns>The created post DTO.</returns>
    public async Task<PostResponseDto> CreatePostAsync(PostDto postDto)
    {
        // Validate the postDto using FluentValidation
        var validationResult = await _validator.ValidateAsync(postDto);

        if (!validationResult.IsValid)
        {
            // Handle validation errors (e.g., throw an exception or return error details)
            throw new ValidationException(validationResult.Errors);
        }

        // Identify the author by name and surname
        var author = await _authorRepository.GetAuthorByNameAndSurnameAsync(postDto.AuthorName, postDto.AuthorSurname);

        // If the author does not exist, create a new author
        if (author == null)
        {
            author = new Author
            {
                Name = postDto.AuthorName,
                Surname = postDto.AuthorSurname
            };

            // Create the new author
            var createdAuthor = await _authorRepository.CreateAuthorAsync(author);
            postDto.AuthorId = createdAuthor.Id; // Assign the new author's ID to the post DTO
        }
        else
        {
            postDto.AuthorId = author.Id; // Use existing author's ID
        }

        var post = _mapper.Map<Post>(postDto);
        var createdPost = await _postRepository.CreatePostAsync(post);
        return _mapper.Map<PostResponseDto>(createdPost);
    }

    /// <summary>
    /// Retrieves a blog post by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the post.</param>
    /// <param name="includeAuthor">Indicates whether to include author information.</param>
    /// <returns>The requested post DTO.</returns>
    public async Task<PostResponseDto> GetPostByIdAsync(Guid id, bool includeAuthor = false)
    {
        var post = await _postRepository.GetPostByIdAsync(id, includeAuthor);
        return _mapper.Map<PostResponseDto>(post);
    }
}