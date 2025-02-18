using FastEndpoints;

namespace RiverBooks.Books;

internal class ListBooksEndpoint(IBookService bookService) : EndpointWithoutRequest<ListBooksResponse>
{
  public override void Configure()
  {
    Get("/books");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    List<BookDTO> books = await bookService.ListBooksAsync();
    await SendAsync(new ListBooksResponse(books), cancellation: cancellationToken);
  }
}
