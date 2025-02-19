using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Infrastructure.Data;
using RiverBooks.Users.Interfaces;
using Serilog;

namespace RiverBooks.Users;

public static class UsersModuleExtensions
{
  public static IServiceCollection AddUserModuleServices(this IServiceCollection services, ConfigurationManager config,
    ILogger logger, List<Assembly> mediatRAssemblies)
  {
    string? connectionString = config.GetConnectionString("UsersConnectionString");
    services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(connectionString));
    services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<UsersDbContext>();

    // Add User Services
    services.AddScoped<IApplicationUserRepository, EfApplicationUserRepository>();
    services.AddScoped<IReadOnlyUserStreetAddressRepository, EfUserStreetAddressRepository>();
    services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

    // If using MediatR in this module, add any assemblies that contain handlers
    mediatRAssemblies.Add(typeof(UsersModuleExtensions).Assembly);

    logger.Information("{Module} module services registered", "Users");

    return services;
  }
}
