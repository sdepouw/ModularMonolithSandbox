using Ardalis.Result;
using MediatR;
using RiverBooks.Books.Contracts;

namespace RiverBooks.Books.Integrations;

internal class BookDetailsQueryHandler(IBookService bookService) : IRequestHandler<BookDetailsQuery, Result<BookDetailsResponse>>
{
  public async Task<Result<BookDetailsResponse>> Handle(BookDetailsQuery request, CancellationToken cancellationToken)
  {
    BookDTO? book = await bookService.GetBookByIdAsync(request.BookId);
    return book is not null
      ? new BookDetailsResponse(book.Id, book.Title, book.Author, book.Price)
      : Result.NotFound();
  }
}
