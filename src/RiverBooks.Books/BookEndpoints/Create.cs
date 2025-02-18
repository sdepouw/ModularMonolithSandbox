using FastEndpoints;

namespace RiverBooks.Books.BookEndpoints;

internal class Create(IBookService bookService) : Endpoint<CreateRequest, BookDTO>
{
  public override void Configure()
  {
    Post("/books");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateRequest request, CancellationToken cancellationToken)
  {
    BookDTO newBook = new(request.Id ?? Guid.NewGuid(), request.Title, request.Author, request.Price);
    await bookService.CreateBookAsync(newBook);
    await SendCreatedAtAsync<GetById>(new { newBook.Id }, newBook, cancellation: cancellationToken);
  }
}
