using BlogSolutionClean.Domain.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogSolutionClean.Infrastructure.Data;

/// <summary>
/// Represents the application's database context.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the DbSet for posts.
    /// </summary>
    public DbSet<Post> Posts { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for authors.
    /// </summary>
    public DbSet<Author> Authors { get; set; }

    /// <summary>
    /// Configures the model.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasKey(p => p.Id);
        modelBuilder.Entity<Author>().HasKey(a => a.Id);
    }
}
