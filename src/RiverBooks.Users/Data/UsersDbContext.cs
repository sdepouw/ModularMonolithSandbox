using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Users.Data;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : IdentityDbContext(options)
{
  internal DbSet<ApplicationUser> ApplicationUsers { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasDefaultSchema("Users"); // TODO: Abandon magic string
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    base.OnModelCreating(modelBuilder);
  }

  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    configurationBuilder.Properties<decimal>()
      .HavePrecision(18, 6);
  }
}
