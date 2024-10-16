using System.ComponentModel.DataAnnotations;

namespace BlogSolutionClean.Application.Dtos;

/// <summary>
/// Data Transfer Object for the Post entity.
/// </summary>
public class PostDto
{

    /// <summary>
    /// Gets or sets the author identifier (UUID).
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// Gets or sets the title of the post.
    /// </summary>
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters.")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the post.
    /// </summary>
    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the content of the post.
    /// </summary>
    [Required(ErrorMessage = "Content is required.")]
    public string Content { get; set; }

    /// <summary>
    /// Gets or sets the name of the author.
    /// </summary>
    [Required(ErrorMessage = "Author name is required.")]
    public string AuthorName { get; set; }

    /// <summary>
    /// Gets or sets the surname of the author.
    /// </summary>
    [Required(ErrorMessage = "Author surname is required.")]
    public string AuthorSurname { get; set; }
}
