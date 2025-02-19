namespace RiverBooks.Books.Interfaces;

internal interface IBookService
{
  Task<List<BookDTO>> ListBooksAsync();
  Task<BookDTO?> GetBookByIdAsync(Guid id);
  Task CreateBookAsync(BookDTO book);
  Task DeleteBookAsync(Guid id);
  Task UpdateBookPriceAsync(Guid bookId, decimal newPrice);
}
