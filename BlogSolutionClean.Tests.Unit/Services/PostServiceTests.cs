using AutoMapper;
using BlogSolutionClean.Application.Services;
using BlogSolutionClean.Domain.Entities.Entities;
using BlogSolutionClean.Infrastructure.Interfaces;
using BlogSolutionClean.Shared.Dtos;
using BlogSolutionClean.Shared.Mappings;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace BlogSolutionClean.Tests.Unit.Services
{
    public class PostServiceTests
    {

        private readonly PostService _postService;
        private readonly Mock<IPostRepository> _mockPostRepository;
        private readonly Mock<IAuthorRepository> _mockAuthorRepository;
        private readonly Mock<IValidator<PostDto>> _validatorMock;
        private readonly IMapper _mapper;

        public PostServiceTests()
        {

            _mockPostRepository = new Mock<IPostRepository>();
            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _validatorMock = new Mock<IValidator<PostDto>>();

            // Set up AutoMapper configuration
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = config.CreateMapper();

            _postService = new PostService(_mapper, _mockPostRepository.Object, _mockAuthorRepository.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task CreatePostAsync_ValidPostDto_CreatesPost()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var postDto = new PostDto
            {
                Title = "Sample Post",
                Description = "Sample Description",
                Content = "Sample Content",
                AuthorName = "John",
                AuthorSurname = "Doe"
            };

            var author = new Author { Id = authorId, Name = "John", Surname = "Doe" };
            var createdPost = new Post { Id = Guid.NewGuid(), Title = postDto.Title };


            // Mock validation to return valid result
            _validatorMock.Setup(v => v.ValidateAsync(postDto, default))
                .ReturnsAsync(new ValidationResult());

            // Set up mock behaviors
            _mockAuthorRepository.Setup(repo => repo.GetAuthorByNameAndSurnameAsync(postDto.AuthorName, postDto.AuthorSurname))
                .ReturnsAsync(author);

            _mockPostRepository.Setup(repo => repo.CreatePostAsync(It.IsAny<Post>()))
                .ReturnsAsync(createdPost);

            // Act
            var result = await _postService.CreatePostAsync(postDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdPost.Id, result.Id);
            _mockPostRepository.Verify(repo => repo.CreatePostAsync(It.IsAny<Post>()), Times.Once);
        }

        [Fact]
        public async Task CreatePostAsync_AuthorDoesNotExist_CreatesNewAuthorAndPost()
        {
            // Arrange
            var postDto = new PostDto
            {
                Title = "New Post",
                Description = "New Description",
                Content = "New Content",
                AuthorName = "Jane",
                AuthorSurname = "Smith"
            };

            var createdAuthor = new Author { Id = Guid.NewGuid(), Name = postDto.AuthorName, Surname = postDto.AuthorSurname };
            var createdPost = new Post { Id = Guid.NewGuid() };


            // Mock validation to return valid result
            _validatorMock.Setup(v => v.ValidateAsync(postDto, default))
                .ReturnsAsync(new ValidationResult());

            // Set up mock behaviors
            _mockAuthorRepository.Setup(repo => repo.GetAuthorByNameAndSurnameAsync(postDto.AuthorName, postDto.AuthorSurname))
                .ReturnsAsync((Author)null); // Author does not exist

            _mockAuthorRepository.Setup(repo => repo.CreateAuthorAsync(It.IsAny<Author>()))
                .ReturnsAsync(createdAuthor);

            _mockPostRepository.Setup(repo => repo.CreatePostAsync(It.IsAny<Post>()))
                .ReturnsAsync(createdPost);

            // Act
            var result = await _postService.CreatePostAsync(postDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdPost.Id, result.Id);
            _mockAuthorRepository.Verify(repo => repo.CreateAuthorAsync(It.IsAny<Author>()), Times.Once);
            _mockPostRepository.Verify(repo => repo.CreatePostAsync(It.IsAny<Post>()), Times.Once);
        }

        [Fact]
        public async Task CreatePostAsync_InvalidPostDto_ThrowsException()
        {
            // Arrange
            var postDto = new PostDto
            {
                Title = null, // Invalid Title
                Description = "Sample Description",
                Content = "Sample Content",
                AuthorName = "John",
                AuthorSurname = "Doe"
            };
            // Mock validation failure
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Title", "Title cannot be null")
            });

            _validatorMock.Setup(v => v.ValidateAsync(postDto, default))
                .ReturnsAsync(validationResult);
            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _postService.CreatePostAsync(postDto));
        }

        [Fact]
        public async Task GetPostByIdAsync_ValidId_ReturnsPostDto()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var post = new Post
            {
                Id = postId,
                Title = "Sample Post",
                Description = "Sample Description",
                Content = "Sample Content",
                AuthorId = Guid.NewGuid()
            };

            _mockPostRepository.Setup(repo => repo.GetPostByIdAsync(postId, false)).ReturnsAsync(post);

            // Act
            var result = await _postService.GetPostByIdAsync(postId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(post.Title, result.Title);
            _mockPostRepository.Verify(repo => repo.GetPostByIdAsync(postId, false), Times.Once);
        }

        [Fact]
        public async Task GetPostByIdAsync_InvalidId_ReturnsNull()
        {
            // Arrange
            var postId = Guid.NewGuid();
            _mockPostRepository.Setup(repo => repo.GetPostByIdAsync(postId, false)).ReturnsAsync((Post)null);

            // Act
            var result = await _postService.GetPostByIdAsync(postId);

            // Assert
            Assert.Null(result);
            _mockPostRepository.Verify(repo => repo.GetPostByIdAsync(postId, false), Times.Once);
        }
    }
}
