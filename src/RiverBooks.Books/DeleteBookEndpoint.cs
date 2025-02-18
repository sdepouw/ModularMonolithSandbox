using FastEndpoints;

namespace RiverBooks.Books;

internal class DeleteBookEndpoint(IBookService bookService) : Endpoint<DeleteBookRequest>
{
  public override void Configure()
  {
    Delete("/books/{Id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(DeleteBookRequest request, CancellationToken cancellationToken)
  {
    await bookService.DeleteBookAsync(request.Id);
    await SendNoContentAsync(cancellationToken);
  }
}