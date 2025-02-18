namespace RiverBooks.Users.UseCases;

internal interface IApplicationUserRepository
{
  Task<ApplicationUser?> GetUserWithCartByEmailAsync(string email);
  Task SaveChangesAsync();
}
