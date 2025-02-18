using FastEndpoints;

namespace RiverBooks.Books;

internal class CreateBookEndpoint(IBookService bookService) : Endpoint<CreateBookRequest, BookDTO>
{
  public override void Configure()
  {
    Post("/books");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateBookRequest request, CancellationToken cancellationToken)
  {
    BookDTO newBook = new(request.Id ?? Guid.NewGuid(), request.Title, request.Author, request.Price);
    await bookService.CreateBookAsync(newBook);
    await SendCreatedAtAsync<GetBookByIdEndpoint>(new { newBook.Id }, newBook, cancellation: cancellationToken);
  }
}
