using FastEndpoints;
using RiverBooks.Books.Interfaces;

namespace RiverBooks.Books.BookEndpoints;

internal class Delete(IBookService bookService) : Endpoint<DeleteRequest>
{
  public override void Configure()
  {
    Delete("/books/{Id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(DeleteRequest request, CancellationToken cancellationToken)
  {
    // TODO: Handle Not Found gracefully
    await bookService.DeleteBookAsync(request.Id);
    await SendNoContentAsync(cancellationToken);
  }
}
