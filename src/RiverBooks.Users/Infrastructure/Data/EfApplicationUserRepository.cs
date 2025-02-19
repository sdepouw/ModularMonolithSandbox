using Microsoft.EntityFrameworkCore;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.Infrastructure.Data;

internal class EfApplicationUserRepository(UsersDbContext dbContext) : IApplicationUserRepository
{
  public Task<ApplicationUser?> GetUserWithCartByEmailAsync(string email)
  {
    return dbContext.ApplicationUsers
      .Include(user => user.CartItems)
      .SingleOrDefaultAsync(user => user.Email == email);
  }

  public Task<ApplicationUser?> GetUserWithAddressesByEmailAsync(string email)
  {
    return dbContext.ApplicationUsers
      .Include(user => user.Addresses)
      .SingleOrDefaultAsync(user => user.Email == email);
  }

  public Task SaveChangesAsync() => dbContext.SaveChangesAsync();
}
