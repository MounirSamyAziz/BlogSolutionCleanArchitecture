using BlogSolutionClean.Domain.Entities.Entities;
using BlogSolutionClean.Shared.Dtos;
using BlogSolutionClean.Tests.Shared;

namespace BlogSolutionClean.Tests.Integration.Services;

/// <summary>
/// Integration tests for the PostService to verify correct handling of post creation and retrieval logic.
/// </summary>
public class PostServiceIntegrationTests : BaseServerFactoryTestClass
{
    /// <summary>
    /// Initializes a new instance of the PostServiceIntegrationTests class.
    /// Seeds the database with initial data, specifically creating an author for use in the tests.
    /// </summary>
    /// <param name="factory">The server factory for setting up the test environment.</param>
    public PostServiceIntegrationTests(BlogSolutionCleanAPIServerFactory factory) : base(factory)
    {
        // Seed the database with initial data, like creating a test author.
        SeedDatabase();
    }

    /// <summary>
    /// Seeds the database by adding a default author for test purposes.
    /// </summary>
    private void SeedDatabase()
    {
        var author = new Author { Name = "John", Surname = "Doe" };
        context.Authors.Add(author);
        context.SaveChanges();
    }

    /// <summary>
    /// Test to verify that a valid PostDto is correctly handled by the service and that a new post is created.
    /// </summary>
    [Fact]
    public async Task CreatePostAsync_ValidPostDto_ShouldCreatePost()
    {
        // Arrange: Set up a new post DTO for testing.
        var postDto = new PostDto
        {
            Title = "Test Post",
            Description = "This is a test post.",
            Content = "Content of the test post.",
            AuthorName = "John",
            AuthorSurname = "Doe"
        };

        // Act: Call the service to create the post.
        var result = await postService.CreatePostAsync(postDto);

        // Assert: Verify that the post was created correctly and the result matches the input.
        Assert.NotNull(result);
        Assert.Equal(postDto.Title, result.Title);
        Assert.Equal(postDto.Description, result.Description);
        Assert.Equal(postDto.Content, result.Content);
        Assert.NotEqual(Guid.Empty, result.Id); // Ensure the ID is not empty, confirming creation.
    }

    /// <summary>
    /// Test to verify that an existing post can be retrieved by its ID.
    /// </summary>
    [Fact]
    public async Task GetPostByIdAsync_ExistingPostId_ShouldReturnPost()
    {
        // Arrange: Create a new post via the service.
        var postDto = new PostDto
        {
            Title = "Test Post",
            Description = "This is a test post.",
            Content = "Content of the test post.",
            AuthorName = "John",
            AuthorSurname = "Doe"
        };

        var createdPost = await postService.CreatePostAsync(postDto);

        // Act: Retrieve the created post by its ID using the service.
        var result = await postService.GetPostByIdAsync(createdPost.Id);

        // Assert: Verify that the retrieved post matches the created post.
        Assert.NotNull(result);
        Assert.Equal(createdPost.Id, result.Id);
        Assert.Equal(postDto.Title, result.Title);
    }
}
