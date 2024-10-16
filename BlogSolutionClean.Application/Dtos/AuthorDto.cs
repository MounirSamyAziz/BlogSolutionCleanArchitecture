using System.ComponentModel.DataAnnotations;

namespace BlogSolutionClean.Application.Dtos;

/// <summary>
/// Data Transfer Object for the Author entity.
/// </summary>
public class AuthorDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the author.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the author.
    /// </summary>
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the surname of the author.
    /// </summary>
    [Required(ErrorMessage = "Surname is required.")]
    public string Surname { get; set; }
}
