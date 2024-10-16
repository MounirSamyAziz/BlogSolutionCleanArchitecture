using BlogSolutionClean.Domain.Entities.Entities;
using BlogSolutionClean.Shared.Dtos;
using BlogSolutionClean.Tests.Shared;
using System.Net.Http.Json;

namespace BlogSolutionClean.Tests.Integration.Controllers;

/// <summary>
/// Integration tests for the PostController to ensure the API behaves as expected.
/// These tests check both the creation and retrieval of posts from the API.
/// </summary>
public class PostControllerIntegrationTests : BaseServerFactoryTestClass
{
    /// <summary>
    /// Initializes a new instance of the PostControllerIntegrationTests class.
    /// Inherits from the BaseServerFactoryTestClass for setting up the test environment.
    /// </summary>
    /// <param name="factory">The server factory for creating test instances of the API.</param>
    public PostControllerIntegrationTests(BlogSolutionCleanAPIServerFactory factory) : base(factory)
    {
    }

    /// <summary>
    /// Test to verify that a post can be successfully created and saved to the database.
    /// </summary>
    [Fact]
    public async Task CreatePost_ShouldSaveToDatabase()
    {
        // Arrange: Prepare a new post DTO to send to the API.
        var newPost = new PostDto
        {
            AuthorId = Guid.NewGuid(),
            Title = "Integration Test Post",
            Description = "Integration Test Description",
            Content = "Integration Test Content",
            AuthorName = "test",
            AuthorSurname = "test",
        };

        // Act: Send a POST request to the API to create the new post.
        var response = await client.PostAsJsonAsync("/api/post", newPost);

        // Assert: Ensure that the response was successful and the post was saved.
        response.EnsureSuccessStatusCode();

        // Retrieve the created post and check that it exists in the database.
        var createdPost = await response.Content.ReadFromJsonAsync<PostResponseDto>();
        var postInDb = await context.Posts.FindAsync(createdPost.Id);
        Assert.NotNull(postInDb);
        Assert.Equal(newPost.Title, postInDb.Title);
    }

    /// <summary>
    /// Test to verify that an existing post can be retrieved by its ID.
    /// </summary>
    [Fact]
    public async Task GetPostById_ShouldReturnExistingPost()
    {
        // Arrange: Create a new post and add it to the database.
        var postId = Guid.NewGuid();
        context.Posts.Add(new Post
        {
            Id = postId,
            AuthorId = Guid.NewGuid(),
            Title = "Existing Post",
            Description = "Description",
            Content = "Content"
        });
        context.SaveChanges();

        // Act: Send a GET request to the API to retrieve the post by its ID.
        var response = await client.GetAsync($"/api/post/{postId}");

        // Assert: Ensure that the response was successful and the post details match.
        response.EnsureSuccessStatusCode();
        var post = await response.Content.ReadFromJsonAsync<PostResponseDto>();
        Assert.Equal(postId, post.Id);
    }
}
