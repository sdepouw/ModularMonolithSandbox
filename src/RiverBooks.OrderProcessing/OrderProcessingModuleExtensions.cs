using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.OrderProcessing.Infrastructure;
using RiverBooks.OrderProcessing.Infrastructure.Data;
using RiverBooks.OrderProcessing.Interfaces;
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
    services.AddScoped<RedisOrderAddressCache>(); // Decorator with concrete class
    services.AddScoped<IOrderAddressCache, ReadThroughOrderAddressCache>();

    // If using MediatR in this module, add any assemblies that contain handlers
    mediatRAssemblies.Add(typeof(OrderProcessingModuleExtensions).Assembly);

    logger.Information("{Module} module services registered", "OrderProcessing");

    return services;
  }
}
