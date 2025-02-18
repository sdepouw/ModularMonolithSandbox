using Ardalis.Result;
using MediatR;

namespace RiverBooks.Books.Contracts;

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