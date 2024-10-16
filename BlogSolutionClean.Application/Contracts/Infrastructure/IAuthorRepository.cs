using BlogSolutionClean.Domain.Entities.Entities;

namespace BlogSolutionClean.Infrastructure.Interfaces;

/// <summary>
/// Represents the contract for author repository operations.
/// Provides methods to interact with author data in the storage.
/// </summary>
public interface IAuthorRepository
{
    /// <summary>
    /// Asynchronously retrieves an author by their name and surname.
    /// </summary>
    /// <param name="name">The name of the author.</param>
    /// <param name="surname">The surname of the author.</param>
    /// <returns>
    /// A <see cref="Task{Author}"/> representing the asynchronous operation.
    /// The task result contains the <see cref="Author"/> object if found; otherwise, null.
    /// </returns>
    Task<Author> GetAuthorByNameAndSurnameAsync(string name, string surname);

    /// <summary>
    /// Asynchronously creates a new author in the storage.
    /// </summary>
    /// <param name="author">The <see cref="Author"/> object to be created.</param>
    /// <returns>
    /// A <see cref="Task{Author}"/> representing the asynchronous operation.
    /// The task result contains the created <see cref="Author"/> object with its assigned ID.
    /// </returns>
    Task<Author> CreateAuthorAsync(Author author);
}
