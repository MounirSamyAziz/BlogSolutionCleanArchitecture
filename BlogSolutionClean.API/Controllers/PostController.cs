using BlogSolutionClean.Application.Interfaces;
using BlogSolutionClean.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BlogSolutionClean.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    /// <summary>
    /// Creates a new blog post.
    /// </summary>
    /// <param name="post">The post object containing title, description, content, and author details.</param>
    /// <returns>The created post object.</returns>
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] PostDto post)
    {
        if (post == null)
            return BadRequest("Post is null.");

        var createdPost = await _postService.CreatePostAsync(post);
        return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, createdPost);
    }

    /// <summary>
    /// Retrieves a blog post by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the post.</param>
    /// <param name="includeAuthor">Whether to include author information in the response.</param>
    /// <returns>The requested post object.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(Guid id, bool includeAuthor = false)
    {
        var post = await _postService.GetPostByIdAsync(id, includeAuthor);
        if (post == null)
            return NotFound();

        return Ok(post);
    }
}
