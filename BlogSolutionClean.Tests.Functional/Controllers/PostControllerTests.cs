using BlogSolutionClean.Shared.Dtos;
using BlogSolutionClean.Tests.Shared;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace BlogSolutionClean.Tests.Functional.Controllers;

public class PostControllerTests : IClassFixture<BlogSolutionCleanAPIServerFactory>
{
    private readonly HttpClient _client;

    public PostControllerTests(BlogSolutionCleanAPIServerFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreatePost_ReturnsCreatedPost()
    {
        var postDto = new PostDto
        {
            Title = "Functional Test Title",
            Description = "Functional Test Description",
            Content = "Functional Test Content",
            AuthorName = "Test Author",
            AuthorSurname = "Test Surname"
        };

        var content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
        var response = await _client.PostAsJsonAsync("/api/post", postDto);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var createdPost = JsonConvert.DeserializeObject<PostResponseDto>(await response.Content.ReadAsStringAsync());
        createdPost.Should().NotBeNull();
        createdPost.Title.Should().Be(postDto.Title);
    }

}
