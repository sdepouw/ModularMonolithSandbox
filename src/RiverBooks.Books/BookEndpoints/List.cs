using FastEndpoints;

namespace RiverBooks.Books.BookEndpoints;

internal class List(IBookService bookService) : EndpointWithoutRequest<ListResponse>
{
  public override void Configure()
  {
    Get("/books");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    List<BookDTO> books = await bookService.ListBooksAsync();
    await SendAsync(new ListResponse(books), cancellation: cancellationToken);
  }
}
