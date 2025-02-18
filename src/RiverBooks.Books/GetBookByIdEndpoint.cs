using FastEndpoints;

namespace RiverBooks.Books;

internal class GetBookByIdEndpoint(IBookService bookService) : Endpoint<GetBookByIdRequest, BookDTO>
{
  public override void Configure()
  {
    Get("/Books/{Id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetBookByIdRequest request, CancellationToken cancellationToken)
  {
    BookDTO? book = await bookService.GetBookByIdAsync(request.Id);
    if (book is null)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }
    await SendAsync(new BookDTO(book.Id, book.Title, book.Author, book.Price), cancellation: cancellationToken);
  }
}
