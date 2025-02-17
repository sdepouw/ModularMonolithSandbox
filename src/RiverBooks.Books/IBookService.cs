namespace RiverBooks.Books;

internal interface IBookService
{
  IEnumerable<BookDTO> ListBooks();
}