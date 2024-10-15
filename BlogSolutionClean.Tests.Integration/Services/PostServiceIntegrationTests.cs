using BlogSolutionClean.Domain.Entities.Entities;
using BlogSolutionClean.Shared.Dtos;
using BlogSolutionClean.Tests.Shared;

namespace BlogSolutionClean.Tests.Integration.Services
{
    public class PostServiceIntegrationTests : BaseServerFactoryTestClass
    {

        public PostServiceIntegrationTests(BlogSolutionCleanAPIServerFactory factory):base(factory) 
        {
            // Seed data
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var author = new Author { Name = "John", Surname = "Doe" };
            context.Authors.Add(author);
            context.SaveChanges();
        }

        [Fact]
        public async Task CreatePostAsync_ValidPostDto_ShouldCreatePost()
        {
            // Arrange
            var postDto = new PostDto
            {
                Title = "Test Post",
                Description = "This is a test post.",
                Content = "Content of the test post.",
                AuthorName = "John",
                AuthorSurname = "Doe"
            };

            // Act
            var result = await postService.CreatePostAsync(postDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(postDto.Title, result.Title);
            Assert.Equal(postDto.Description, result.Description);
            Assert.Equal(postDto.Content, result.Content);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task GetPostByIdAsync_ExistingPostId_ShouldReturnPost()
        {
            // Arrange
            var postDto = new PostDto
            {
                Title = "Test Post",
                Description = "This is a test post.",
                Content = "Content of the test post.",
                AuthorName = "John",
                AuthorSurname = "Doe"
            };

            var createdPost = await postService.CreatePostAsync(postDto);

            // Act
            var result = await postService.GetPostByIdAsync(createdPost.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdPost.Id, result.Id);
            Assert.Equal(postDto.Title, result.Title);
        }

    }
}
