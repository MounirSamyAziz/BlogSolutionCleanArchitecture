using AutoMapper;
using BlogSolutionClean.Application.Services;
using BlogSolutionClean.Domain.Entities.Entities;
using BlogSolutionClean.Infrastructure.Interfaces;
using BlogSolutionClean.Shared.Dtos;
using BlogSolutionClean.Shared.Mappings;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace BlogSolutionClean.Tests.Unit.Services;

public class AuthorServiceTests
{
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly IMapper _mapper;
    private readonly AuthorService _authorService;
    private readonly Mock<IValidator<AuthorDto>> _validatorMock;

    public AuthorServiceTests()
    {
        // Initialize the mock repository
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _validatorMock = new Mock<IValidator<AuthorDto>>();

        // Set up AutoMapper configuration
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = config.CreateMapper();

        // Initialize the service with mock dependencies
        _authorService = new AuthorService(_mapper, _authorRepositoryMock.Object, _validatorMock.Object);
    }

    /// <summary>
    /// Test to verify if an author is created successfully.
    /// </summary>
    [Fact]
    public async Task CreateAuthorAsync_ValidAuthorDto_ReturnsAuthorDto()
    {
        // Arrange
        var authorDto = new AuthorDto
        {
            Name = "John",
            Surname = "Doe"
        };

        var author = new Author
        {
            Id = new Guid(),
            Name = "John",
            Surname = "Doe"
        };

        // Mock validation to return valid result
        _validatorMock.Setup(v => v.ValidateAsync(authorDto, default))
            .ReturnsAsync(new ValidationResult());

        // Set up the repository mock to return the author when creating
        _authorRepositoryMock
            .Setup(repo => repo.CreateAuthorAsync(It.IsAny<Author>()))
            .ReturnsAsync(author);
        // Act
        var result = await _authorService.CreateAuthorAsync(authorDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(authorDto.Name, result.Name);
        Assert.Equal(authorDto.Surname, result.Surname);

        // Verify that CreateAuthorAsync was called once
        _authorRepositoryMock.Verify(repo => repo.CreateAuthorAsync(It.IsAny<Author>()), Times.Once);
    }

    /// <summary>
    /// Test to verify behavior when invalid authorDto is provided.
    /// </summary>
    [Fact]
    public async Task CreateAuthorAsync_InvalidAuthorDto_ThrowsArgumentException()
    {
        // Arrange
        var authorDto = new AuthorDto
        {
            Name = null,
            Surname = "Doe"
        };


        // Mock validation failure
        var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Title", "Title cannot be null")
            });

        _validatorMock.Setup(v => v.ValidateAsync(authorDto, default))
            .ReturnsAsync(validationResult);

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => _authorService.CreateAuthorAsync(authorDto));

        // Verify that the repository was never called
        _authorRepositoryMock.Verify(repo => repo.CreateAuthorAsync(It.IsAny<Author>()), Times.Never);
    }
}
