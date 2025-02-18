using Ardalis.GuardClauses;

namespace RiverBooks.Books;

internal class BookService(IBookRepository bookRepository) : IBookService
{
  public async Task<List<BookDTO>> ListBooksAsync()
  {
    List<Book> books = await bookRepository.ListAsync();
    return books
      .Select(book => new BookDTO(book.Id, book.Title, book.Author, book.Price))
      .ToList();
  }

  public async Task<BookDTO> GetBookByIdAsync(Guid id)
  {
    Book? book = await bookRepository.GetByIdAsync(id);
    Guard.Against.Null(book); // TODO: Handle not found case better (return Not Found)
    return new BookDTO(book.Id, book.Title, book.Author, book.Price);
  }

  public async Task CreateBookAsync(BookDTO book)
  {
    await bookRepository.AddAsync(new(book.Id, book.Title, book.Author, book.Price));
    await bookRepository.SaveChangesAsync();
  }

  public async Task DeleteBookAsync(Guid id)
  {
    Book? book = await bookRepository.GetByIdAsync(id);
    if (book is not null)
    {
      await bookRepository.DeleteAsync(book);
      await bookRepository.SaveChangesAsync();
    }
  }

  public async Task UpdateBookPriceAsync(Guid bookId, decimal newPrice)
  {
    // TODO: Validate the price
    Book? book = await bookRepository.GetByIdAsync(bookId);
    Guard.Against.Null(book); // TODO: Handle not found case better (return Not Found)
    book.UpdatePrice(newPrice);
    await bookRepository.SaveChangesAsync();
  }
}
