using BlogSolutionClean.Domain.Entities.Entities;
using BlogSolutionClean.Infrastructure.Data;
using BlogSolutionClean.Shared.Dtos;
using BlogSolutionClean.Tests.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace BlogSolutionClean.Tests.Integration.Controllers;
public class PostControllerIntegrationTests : BaseServerFactoryTestClass
{

    public PostControllerIntegrationTests(BlogSolutionCleanAPIServerFactory factory):base(factory)
    {
    }

    [Fact]
    public async Task CreatePost_ShouldSaveToDatabase()
    {
        // Arrange
        var newPost = new PostDto
        {
            AuthorId = Guid.NewGuid(),
            Title = "Integration Test Post",
            Description = "Integration Test Description",
            Content = "Integration Test Content",
            AuthorName = "test",
            AuthorSurname = "test",
        };

        // Act
        var response = await client.PostAsJsonAsync("/api/post", newPost);

        // Assert
        response.EnsureSuccessStatusCode();

        var createdPost = await response.Content.ReadFromJsonAsync<PostResponseDto>();
        var postInDb = await context.Posts.FindAsync(createdPost.Id);
        Assert.NotNull(postInDb);
        Assert.Equal(newPost.Title, postInDb.Title);
    }

    [Fact]
    public async Task GetPostById_ShouldReturnExistingPost()
    {
        // Arrange
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

        // Act
        var response = await client.GetAsync($"/api/post/{postId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var post = await response.Content.ReadFromJsonAsync<PostResponseDto>();
        Assert.Equal(postId, post.Id);
    }
}
