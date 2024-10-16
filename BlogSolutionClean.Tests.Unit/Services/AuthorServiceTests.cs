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

/// <summary>
/// Unit tests for the AuthorService to verify the behavior of author creation, including validation and repository interaction.
/// </summary>
public class AuthorServiceTests
{
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly IMapper _mapper;
    private readonly AuthorService _authorService;
    private readonly Mock<IValidator<AuthorDto>> _validatorMock;

    /// <summary>
    /// Initializes a new instance of the AuthorServiceTests class.
    /// Sets up mocked dependencies, including the author repository, AutoMapper, and a validator for AuthorDto.
    /// </summary>
    public AuthorServiceTests()
    {
        // Initialize the mock repository
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _validatorMock = new Mock<IValidator<AuthorDto>>();

        // Set up AutoMapper configuration for mapping between DTOs and entities
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = config.CreateMapper();

        // Initialize the service with mock dependencies
        _authorService = new AuthorService(_mapper, _authorRepositoryMock.Object, _validatorMock.Object);
    }

    /// <summary>
    /// Test to verify if an author is successfully created when a valid AuthorDto is provided.
    /// </summary>
    [Fact]
    public async Task CreateAuthorAsync_ValidAuthorDto_ReturnsAuthorDto()
    {
        // Arrange: Set up the author DTO and the corresponding entity.
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

        // Mock validation to return a valid result for the provided authorDto.
        _validatorMock.Setup(v => v.ValidateAsync(authorDto, default))
            .ReturnsAsync(new ValidationResult());

        // Set up the repository mock to return the author entity when the CreateAuthorAsync method is called.
        _authorRepositoryMock
            .Setup(repo => repo.CreateAuthorAsync(It.IsAny<Author>()))
            .ReturnsAsync(author);

        // Act: Call the service's CreateAuthorAsync method.
        var result = await _authorService.CreateAuthorAsync(authorDto);

        // Assert: Ensure that the result is not null and that the returned values match the input DTO.
        Assert.NotNull(result);
        Assert.Equal(authorDto.Name, result.Name);
        Assert.Equal(authorDto.Surname, result.Surname);

        // Verify that the repository's CreateAuthorAsync method was called exactly once.
        _authorRepositoryMock.Verify(repo => repo.CreateAuthorAsync(It.IsAny<Author>()), Times.Once);
    }

    /// <summary>
    /// Test to verify that an exception is thrown when an invalid AuthorDto is provided.
    /// </summary>
    [Fact]
    public async Task CreateAuthorAsync_InvalidAuthorDto_ThrowsValidationException()
    {
        // Arrange: Set up an invalid authorDto with a missing name.
        var authorDto = new AuthorDto
        {
            Name = null,
            Surname = "Doe"
        };

        // Mock a validation failure to simulate invalid input.
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name cannot be null")
        });

        _validatorMock.Setup(v => v.ValidateAsync(authorDto, default))
            .ReturnsAsync(validationResult);

        // Act & Assert: Verify that a ValidationException is thrown when attempting to create the invalid author.
        await Assert.ThrowsAsync<ValidationException>(() => _authorService.CreateAuthorAsync(authorDto));

        // Verify that the repository's CreateAuthorAsync method was never called.
        _authorRepositoryMock.Verify(repo => repo.CreateAuthorAsync(It.IsAny<Author>()), Times.Never);
    }
}
