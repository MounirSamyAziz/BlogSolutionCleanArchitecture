using BlogSolutionClean.Shared.Dtos;
using AutoMapper;
using BlogSolutionClean.Domain.Entities.Entities;

namespace BlogSolutionClean.Shared.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between domain entities and DTOs.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            // Create maps between Post and PostDto
            CreateMap<Post, PostDto>().ReverseMap();


            CreateMap<Post, PostResponseDto>().ReverseMap();

            // Create maps between Author and AuthorDto
            CreateMap<Author, AuthorDto>().ReverseMap();

        }
    }
}
