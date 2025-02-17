using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace RiverBooks.Books;

public static class BookEndpoints
{
  public static void MapBookEndpoints(this WebApplication app)
  {
    app.MapGet("/books", (IBookService bookService) => bookService.ListBooks());
  }
}

public static class BookServiceExtensions
{
  public static IServiceCollection AddBookServices(this IServiceCollection services)
  {
    services.AddScoped<IBookService, BookService>();
    return services;
  }
}

internal interface IBookService
{
  IEnumerable<BookDTO> ListBooks();
}

public record BookDTO(Guid Id, string Title, string Author);

internal class BookService : IBookService
{
  public IEnumerable<BookDTO> ListBooks() =>
  [
    new BookDTO(Guid.NewGuid(), "The Fellowship of the Ring", "J.R.R. Tolkien"),
    new BookDTO(Guid.NewGuid(), "The Two Towers", "J.R.R. Tolkien"),
    new BookDTO(Guid.NewGuid(), "The Return of the King", "J.R.R. Tolkien"),
  ];
}
