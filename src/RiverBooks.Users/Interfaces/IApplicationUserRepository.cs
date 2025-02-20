using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Interfaces;

internal interface IApplicationUserRepository
{
  ValueTask<ApplicationUser?> GetUserByIdAsync(Guid userId);
  Task<ApplicationUser?> GetUserWithCartByEmailAsync(string email);
  Task<ApplicationUser?> GetUserWithAddressesByEmailAsync(string email);
  Task SaveChangesAsync();
}
