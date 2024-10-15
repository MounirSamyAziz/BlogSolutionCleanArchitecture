using BlogSolutionClean.Shared.Dtos;

namespace BlogSolutionClean.Application.Interfaces;

/// <summary>
/// Interface for Post service operations.
/// </summary>
public interface IPostService
{
    /// <summary>
    /// Creates a new blog post along with the author information.
    /// </summary>
    /// <param name="postDto">The post data to create, including author information.</param>
    /// <returns>The created post DTO.</returns>
    Task<PostResponseDto> CreatePostAsync(PostDto postDto);

    /// <summary>
    /// Retrieves a blog post by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the post.</param>
    /// <param name="includeAuthor">Indicates whether to include author information.</param>
    /// <returns>The requested post DTO.</returns>
    Task<PostResponseDto> GetPostByIdAsync(Guid id, bool includeAuthor = false);
}