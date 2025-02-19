using Microsoft.EntityFrameworkCore;
using RiverBooks.Books.Domain;
using RiverBooks.Books.Interfaces;

namespace RiverBooks.Books.Infrastructure.Data;

internal class EfBookRepository(BookDbContext dbContext) : IBookRepository
{
  public async Task<Book?> GetByIdAsync(Guid id)
  {
    return await dbContext.Books.FindAsync(id);
  }

  public async Task<List<Book>> ListAsync()
  {
    return await dbContext.Books.ToListAsync();
  }

  public Task AddAsync(Book book)
  {
    dbContext.Add(book);
    return Task.CompletedTask;
  }

  public Task DeleteAsync(Book book)
  {
    dbContext.Remove(book);
    return Task.CompletedTask;
  }

  public async Task SaveChangesAsync()
  {
    await dbContext.SaveChangesAsync();
  }
}
