using BlogSolutionClean.API.Controllers;
using BlogSolutionClean.Application.Interfaces;
using BlogSolutionClean.Shared.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BlogSolutionClean.Tests.Unit.Controllers;
public class PostControllerTests
{
    private readonly Mock<IPostService> _postServiceMock;
    private readonly PostController _postController;

    public PostControllerTests()
    {
        _postServiceMock = new Mock<IPostService>();
        _postController = new PostController(_postServiceMock.Object);
    }

    [Fact]
    public async Task CreatePost_ValidPost_ReturnsCreatedResult()
    {
        // Arrange
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

        _postServiceMock.Setup(s => s.CreatePostAsync(postDto)).ReturnsAsync(createdPostDto);

        // Act
        var result = await _postController.CreatePost(postDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<PostResponseDto>(createdAtActionResult.Value);
        Assert.Equal(createdPostDto.Id, returnValue.Id);
        Assert.Equal(postDto.Title, returnValue.Title);
    }

    [Fact]
    public async Task CreatePost_NullPost_ReturnsBadRequest()
    {
        // Act
        var result = await _postController.CreatePost(null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Post is null.", badRequestResult.Value);
    }

    [Fact]
    public async Task GetPostById_ExistingPost_ReturnsOkResult()
    {
        // Arrange
        var postId = Guid.NewGuid();
        var postDto = new PostResponseDto
        {
            Id = postId,
            Title = "Sample Title",
            Description = "Sample Description",
            Content = "Sample Content",
            AuthorId = Guid.NewGuid() // Simulate author ID
        };

        _postServiceMock.Setup(s => s.GetPostByIdAsync(postId, false)).ReturnsAsync(postDto);

        // Act
        var result = await _postController.GetPostById(postId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<PostResponseDto>(okResult.Value);
        returnValue.Id.Should().Be(postId);
        Assert.Equal(postDto.Title, returnValue.Title);
    }

    [Fact]
    public async Task GetPostById_NonExistingPost_ReturnsNotFound()
    {
        // Arrange
        var postId = Guid.NewGuid();
        _postServiceMock.Setup(s => s.GetPostByIdAsync(postId, false)).Returns(Task.FromResult<PostResponseDto>(null));

        // Act
        var result = await _postController.GetPostById(postId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}