using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.OrderProcessing.Data;
using RiverBooks.OrderProcessing.Integrations;
using RiverBooks.OrderProcessing.MaterializedViews;
using Serilog;

namespace RiverBooks.OrderProcessing;


public static class OrderProcessingModuleExtensions
{
  public static IServiceCollection AddOrderProcessingModuleServices(this IServiceCollection services,
    ConfigurationManager config, ILogger logger, List<Assembly> mediatRAssemblies)
  {
    string? connectionString = config.GetConnectionString("OrderProcessingConnectionString");
    services.AddDbContext<OrderProcessingDbContext>(options => options.UseSqlServer(connectionString));

    // Add User Services
    services.AddScoped<IOrderRepository, EfOrderRepository>();
    services.AddScoped<IOrderAddressCache, RedisOrderAddressCache>();

    // If using MediatR in this module, add any assemblies that contain handlers
    mediatRAssemblies.Add(typeof(OrderProcessingModuleExtensions).Assembly);

    logger.Information("{Module} module services registered", "OrderProcessing");

    return services;
  }
}
