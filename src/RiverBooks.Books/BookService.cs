namespace RiverBooks.Books;

internal class BookService : IBookService
{
  public IEnumerable<BookDTO> ListBooks() =>
  [
    new BookDTO(Guid.NewGuid(), "The Fellowship of the Ring", "J.R.R. Tolkien"),
    new BookDTO(Guid.NewGuid(), "The Two Towers", "J.R.R. Tolkien"),
    new BookDTO(Guid.NewGuid(), "The Return of the King", "J.R.R. Tolkien"),
  ];
}