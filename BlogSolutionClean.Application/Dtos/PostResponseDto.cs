namespace BlogSolutionClean.Application.Dtos;

public class PostResponseDto : PostDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the post.
    /// </summary>
    public Guid Id { get; set; }
}
