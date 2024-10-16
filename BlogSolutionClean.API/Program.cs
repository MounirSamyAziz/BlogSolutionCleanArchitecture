using BlogSolutionClean.Application.Mappings;
using BlogSolutionClean.Infrastructure;
using BlogSolutionClean.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(); // Optional: if you want to use Newtonsoft.Json for serialization


builder.Services.AddServices().AddRepositories();
// in memory database
builder.Services.AddInMemoryDatabase();
//// sql server database
//builder.Services.AddSqlServerDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));


// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Add AutoMapper with the mapping profiles
builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();


// Step 2: Ensure the database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    dbContext.Database.Migrate(); // Apply migrations and create database if it does not exist
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Enable middleware to serve generated Swagger as a JSON endpoint
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API V1");
   // c.RoutePrefix = string.Empty; // Set the Swagger UI at the app's root
});
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }