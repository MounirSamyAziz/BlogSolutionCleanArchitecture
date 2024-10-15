using BlogSolutionClean.Domain.Entities.Entities;
using BlogSolutionClean.Infrastructure.Data;
using BlogSolutionClean.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogSolutionClean.Infrastructure.Repositories;

/// <summary>
/// Repository for managing blog posts.
/// </summary>
public class PostRepository : IPostRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="PostRepository"/> class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public PostRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new post in the database.
    /// </summary>
    /// <param name="post">The post to create.</param>
    /// <returns>The created post.</returns>
    public async Task<Post> CreatePostAsync(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    /// <summary>
    /// Retrieves a post by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the post.</param>
    /// <param name="includeAuthor">Whether to include author information in the result.</param>
    /// <returns>The requested post.</returns>
    public async Task<Post> GetPostByIdAsync(Guid id, bool includeAuthor = false)
    {
        if (includeAuthor)
        {
            return await _context.Posts.Include(p => p.Author).FirstOrDefaultAsync(p => p.Id == id);
        }
        return await _context.Posts.FindAsync(id);
    }
}
