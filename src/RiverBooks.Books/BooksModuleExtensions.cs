using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Books.Infrastructure.Data;
using RiverBooks.Books.Interfaces;
using Serilog;

namespace RiverBooks.Books;

public static class BooksModuleExtensions
{
  public static IServiceCollection AddBookModuleServices(this IServiceCollection services, ConfigurationManager config,
    ILogger logger, List<Assembly> mediatRAssemblies)
  {
    string? connectionString = config.GetConnectionString("BooksConnectionString");
    services.AddDbContext<BookDbContext>(options => options.UseSqlServer(connectionString));
    services.AddScoped<IBookRepository, EfBookRepository>();
    services.AddScoped<IBookService, BookService>();

    // If using MediatR in this module, add any assemblies that contain handlers
    mediatRAssemblies.Add(typeof(BooksModuleExtensions).Assembly);

    logger.Information("{Module} module services registered", "Books");
    return services;
  }
}
