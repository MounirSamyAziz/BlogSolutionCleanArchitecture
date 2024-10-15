using BlogSolutionClean.Application.Interfaces;
using BlogSolutionClean.Application.Services;
using BlogSolutionClean.Application.Validators;
using BlogSolutionClean.Infrastructure.Data;
using BlogSolutionClean.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlogSolutionClean.Infrastructure.Interfaces;

namespace BlogSolutionClean.Application;

public static class ServiceCollectionExtensions
{


    /// <summary>
    /// Adds the application services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection AddInMemoryDatabase(this IServiceCollection services)
    {
        // Register the in-memory database context
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("BlogInMemoryDb"));

        return services;
    }

    /// <summary>
    /// Registers the application services in the service collection.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IAuthorService, AuthorService>();

        return services;
    }

    /// <summary>
    /// Registers the repositories in the service collection.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPostRepository,PostRepository>();
        services.AddScoped<IAuthorRepository,AuthorRepository>();

        services.AddValidatorsFromAssemblyContaining<PostDtoFluentValidator>();
        services.AddValidatorsFromAssemblyContaining<AuthorDtoFluentValidator>();
        
        services.AddFluentValidationAutoValidation();
        return services;
    }

    /// <summary>
    /// Adds the database context using a connection string for a SQL Server database.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="connectionString">The connection string for the SQL Server database.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection AddSqlServerDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        return services;
    }

}
