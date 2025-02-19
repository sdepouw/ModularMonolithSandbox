using RiverBooks.Books.Domain;

namespace RiverBooks.Books.Interfaces;

internal interface IBookRepository : IReadOnlyBookRepository
{
  Task AddAsync(Book book);
  Task DeleteAsync(Book book);
  Task SaveChangesAsync();
}
