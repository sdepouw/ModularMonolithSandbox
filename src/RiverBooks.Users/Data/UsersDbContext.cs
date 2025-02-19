using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Users.Data;

public class UsersDbContext(DbContextOptions<UsersDbContext> options, IDomainEventDispatcher? dispatcher) : IdentityDbContext(options)
{
  internal DbSet<ApplicationUser> ApplicationUsers { get; set; }
  internal DbSet<UserStreetAddress> UserStreetAddresses { get; set; }

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

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // Ignore events if no dispatcher provided
    if (dispatcher == null) return result;

    // Dispatch events only if save was successful
    IHaveDomainEvents[] entitiesWithEvents = ChangeTracker.Entries<IHaveDomainEvents>()
      .Select(e => e.Entity)
      .Where(e => e.DomainEvents.Any())
      .ToArray();

    await dispatcher.DispatchAndClearEvents(entitiesWithEvents);
    return result;
  }
}
