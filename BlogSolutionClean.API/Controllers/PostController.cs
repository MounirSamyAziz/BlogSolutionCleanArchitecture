using BlogSolutionClean.Application.Interfaces;
using BlogSolutionClean.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BlogSolutionClean.API.Controllers;

/// <summary>
/// Controller for managing blog posts.
/// Provides endpoints to create and retrieve blog posts.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService; // Service for handling post operations

    /// <summary>
    /// Initializes a new instance of the <see cref="PostController"/> class.
    /// </summary>
    /// <param name="postService">The service responsible for post operations.</param>
    public PostController(IPostService postService)
    {
        _postService = postService; // Dependency injection of the post service
    }

    /// <summary>
    /// Creates a new blog post.
    /// </summary>
    /// <param name="post">The post object containing title, description, content, and author details.</param>
    /// <returns>An IActionResult containing the created post object or a BadRequest if the post is null.</returns>
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] PostDto post)
    {
        // Check if the post object is null
        if (post == null)
            return BadRequest("Post is null."); // Return a bad request response

        // Call the service to create the post asynchronously
        var createdPost = await _postService.CreatePostAsync(post);
        // Return a CreatedAtAction response with the newly created post
        return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, createdPost);
    }

    /// <summary>
    /// Retrieves a blog post by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the post.</param>
    /// <param name="includeAuthor">Whether to include author information in the response.</param>
    /// <returns>An IActionResult containing the requested post object or a NotFound if the post does not exist.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(Guid id, bool includeAuthor = false)
    {
        // Call the service to retrieve the post by its ID
        var post = await _postService.GetPostByIdAsync(id, includeAuthor);

        // Check if the post was found
        if (post == null)
            return NotFound(); // Return a NotFound response if the post is not found

        // Return an Ok response with the retrieved post
        return Ok(post);
    }
}
