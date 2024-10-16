using BlogSolutionClean.API.Controllers;
using BlogSolutionClean.Application.Interfaces;
using BlogSolutionClean.Shared.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BlogSolutionClean.Tests.Unit.Controllers;

/// <summary>
/// Unit tests for the PostController, testing the behavior of actions like creating and retrieving posts.
/// </summary>
public class PostControllerTests
{
    private readonly Mock<IPostService> _postServiceMock;
    private readonly PostController _postController;

    /// <summary>
    /// Initializes a new instance of the PostControllerTests class.
    /// Sets up the mock PostService and the PostController with dependencies.
    /// </summary>
    public PostControllerTests()
    {
        _postServiceMock = new Mock<IPostService>();
        _postController = new PostController(_postServiceMock.Object);
    }

    /// <summary>
    /// Test to verify that a valid post is created successfully, and a CreatedAtActionResult is returned.
    /// </summary>
    [Fact]
    public async Task CreatePost_ValidPost_ReturnsCreatedResult()
    {
        // Arrange: Set up the post DTO and simulate the created post response.
        var postDto = new PostDto
        {
            Title = "Sample Title",
            Description = "Sample Description",
            Content = "Sample Content",
            AuthorName = "John",
            AuthorSurname = "Doe"
        };

        var createdPostDto = new PostResponseDto
        {
            Id = Guid.NewGuid(), // Simulate the created post ID
            Title = postDto.Title,
            Description = postDto.Description,
            Content = postDto.Content,
            AuthorId = Guid.NewGuid() // Simulate author ID
        };

        // Mock the service to return the created post DTO when CreatePostAsync is called.
        _postServiceMock.Setup(s => s.CreatePostAsync(postDto)).ReturnsAsync(createdPostDto);

        // Act: Call the controller's CreatePost method.
        var result = await _postController.CreatePost(postDto);

        // Assert: Verify that the result is a CreatedAtActionResult, and that the returned post matches the expected values.
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<PostResponseDto>(createdAtActionResult.Value);
        Assert.Equal(createdPostDto.Id, returnValue.Id);
        Assert.Equal(postDto.Title, returnValue.Title);
    }

    /// <summary>
    /// Test to verify that a null post returns a BadRequestObjectResult with an appropriate error message.
    /// </summary>
    [Fact]
    public async Task CreatePost_NullPost_ReturnsBadRequest()
    {
        // Act: Call the controller's CreatePost method with a null argument.
        var result = await _postController.CreatePost(null);

        // Assert: Verify that the result is a BadRequestObjectResult with the expected error message.
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Post is null.", badRequestResult.Value);
    }

    /// <summary>
    /// Test to verify that an existing post is retrieved successfully, and an OkObjectResult is returned.
    /// </summary>
    [Fact]
    public async Task GetPostById_ExistingPost_ReturnsOkResult()
    {
        // Arrange: Set up the post ID and simulate the returned post DTO.
        var postId = Guid.NewGuid();
        var postDto = new PostResponseDto
        {
            Id = postId,
            Title = "Sample Title",
            Description = "Sample Description",
            Content = "Sample Content",
            AuthorId = Guid.NewGuid() // Simulate author ID
        };

        // Mock the service to return the post DTO when GetPostByIdAsync is called with the given postId.
        _postServiceMock.Setup(s => s.GetPostByIdAsync(postId, false)).ReturnsAsync(postDto);

        // Act: Call the controller's GetPostById method.
        var result = await _postController.GetPostById(postId);

        // Assert: Verify that the result is an OkObjectResult, and that the returned post matches the expected values.
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<PostResponseDto>(okResult.Value);
        returnValue.Id.Should().Be(postId);
        Assert.Equal(postDto.Title, returnValue.Title);
    }

    /// <summary>
    /// Test to verify that a non-existing post returns a NotFoundResult.
    /// </summary>
    [Fact]
    public async Task GetPostById_NonExistingPost_ReturnsNotFound()
    {
        // Arrange: Set up a non-existing post ID and simulate the service returning null.
        var postId = Guid.NewGuid();
        _postServiceMock.Setup(s => s.GetPostByIdAsync(postId, false)).Returns(Task.FromResult<PostResponseDto>(null));

        // Act: Call the controller's GetPostById method with the non-existing postId.
        var result = await _postController.GetPostById(postId);

        // Assert: Verify that the result is a NotFoundResult.
        Assert.IsType<NotFoundResult>(result);
    }
}
