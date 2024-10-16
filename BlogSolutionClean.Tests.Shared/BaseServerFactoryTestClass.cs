using AutoMapper;
using BlogSolutionClean.Application.Contracts.Interfaces;
using BlogSolutionClean.Infrastructure.Data;
using BlogSolutionClean.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BlogSolutionClean.Tests.Shared;


public class BaseServerFactoryTestClass : IClassFixture<BlogSolutionCleanAPIServerFactory>, IDisposable
{
    protected readonly HttpClient client;
    protected readonly IPostService postService;
    protected readonly IServiceScope scope;
    protected readonly ApplicationDbContext context;
    protected readonly IAuthorRepository authorRepository;
    protected readonly IMapper mapper;

    public BaseServerFactoryTestClass(BlogSolutionCleanAPIServerFactory factory)
    {
        scope = factory.Services.CreateScope();
        context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        postService = scope.ServiceProvider.GetRequiredService<IPostService>();
        authorRepository = scope.ServiceProvider.GetRequiredService<IAuthorRepository>();
        mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
        client = factory.CreateClient();
    }
    public void Dispose()
    {
        scope.Dispose();
    }

}