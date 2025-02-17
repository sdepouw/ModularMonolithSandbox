namespace RiverBooks.Books;

internal class BookService : IBookService
{
  public List<BookDTO> ListBooks() =>
  [
    new(Guid.NewGuid(), "The Fellowship of the Ring", "J.R.R. Tolkien"),
    new(Guid.NewGuid(), "The Two Towers", "J.R.R. Tolkien"),
    new(Guid.NewGuid(), "The Return of the King", "J.R.R. Tolkien"),
  ];
}
