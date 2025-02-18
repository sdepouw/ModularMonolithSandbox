using FastEndpoints;

namespace RiverBooks.Books.BookEndpoints;

internal class GetById(IBookService bookService) : Endpoint<GetByIdRequest, BookDTO>
{
  public override void Configure()
  {
    Get("/Books/{Id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetByIdRequest request, CancellationToken cancellationToken)
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
