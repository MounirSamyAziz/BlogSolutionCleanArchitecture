using BlogSolutionClean.Application.Dtos;

namespace BlogSolutionClean.Application.Contracts.Interfaces
{
    /// <summary>
    /// Interface for Author service operations.
    /// </summary>
    public interface IAuthorService
    {
        /// <summary>
        /// Creates a new author.
        /// </summary>
        /// <param name="authorDto">The author data to create.</param>
        /// <returns>The created author DTO.</returns>
        Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto);
    }
}
