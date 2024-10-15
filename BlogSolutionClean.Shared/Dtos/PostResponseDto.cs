using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSolutionClean.Shared.Dtos
{
    public class PostResponseDto : PostDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the post.
        /// </summary>
        public Guid Id { get; set; }
    }
}
