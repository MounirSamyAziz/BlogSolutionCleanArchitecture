namespace BlogSolutionClean.Domain.Entities.Entities;

/// <summary>
/// Represents an author of blog posts in the system.
/// </summary>
public class Author
{
    /// <summary>
    /// Gets or sets the unique identifier for the author.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the author.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the surname of the author.
    /// </summary>
    public string Surname { get; set; }

}