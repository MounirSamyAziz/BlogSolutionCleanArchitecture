using BlogSolutionClean.Domain.Entities.Entities;
using BlogSolutionClean.Infrastructure.Data;
using BlogSolutionClean.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogSolutionClean.Infrastructure.Repositories
{

    /// <summary>
    /// Repository for managing authors.
    /// </summary>
    public class AuthorRepository: IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets an author by their name and surname.
        /// </summary>
        /// <param name="name">The author's name.</param>
        /// <param name="surname">The author's surname.</param>
        /// <returns>The author if found; otherwise, null.</returns>
        public async Task<Author> GetAuthorByNameAndSurnameAsync(string name, string surname)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Name == name && a.Surname == surname);
        }

        /// <summary>
        /// Creates a new author in the database.
        /// </summary>
        /// <param name="author">The author to create.</param>
        /// <returns>The created author.</returns>
        public async Task<Author> CreateAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }
    }
}
