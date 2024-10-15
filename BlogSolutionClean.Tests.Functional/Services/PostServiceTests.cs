using BlogSolutionClean.Shared.Dtos;
using BlogSolutionClean.Tests.Shared;
using FluentAssertions;

namespace BlogSolutionClean.Tests.Functional.Services
{
    public class PostServiceTests : BaseServerFactoryTestClass
    {
        public PostServiceTests(BlogSolutionCleanAPIServerFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task CreatePostAsync_ShouldReturnPostDto()
        {
            var postDto = new PostDto
            {
                Title = "Post Title",
                Description = "Post Description",
                Content = "Post Content",
                AuthorName = "Author Name",
                AuthorSurname = "Author Surname"
            };

            var createdPost = await postService.CreatePostAsync(postDto);
            createdPost.Should().NotBeNull();
            createdPost.Title.Should().Be(postDto.Title);
        }


    }
}
