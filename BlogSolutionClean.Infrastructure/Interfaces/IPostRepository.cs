using BlogSolutionClean.Domain.Entities.Entities;

namespace BlogSolutionClean.Infrastructure.Interfaces
{
    /// <summary>
    /// Represents the contract for post repository operations.
    /// Provides methods to interact with blog post data in the storage.
    /// </summary>
    public interface IPostRepository
    {
        /// <summary>
        /// Asynchronously creates a new blog post in the storage.
        /// </summary>
        /// <param name="post">The <see cref="Post"/> object to be created.</param>
        /// <returns>
        /// A <see cref="Task{Post}"/> representing the asynchronous operation.
        /// The task result contains the created <see cref="Post"/> object with its assigned ID.
        /// </returns>
        Task<Post> CreatePostAsync(Post post);

        /// <summary>
        /// Asynchronously retrieves a blog post by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the post.</param>
        /// <param name="includeAuthor">
        /// A boolean value indicating whether to include author information in the result.
        /// Defaults to false.
        /// </param>
        /// <returns>
        /// A <see cref="Task{Post}"/> representing the asynchronous operation.
        /// The task result contains the <see cref="Post"/> object if found; otherwise, null.
        /// </returns>
        Task<Post> GetPostByIdAsync(Guid id, bool includeAuthor = false);
    }
}
