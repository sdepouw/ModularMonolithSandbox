using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Reporting.ReportEndpoints;
using Serilog;

namespace RiverBooks.Reporting;

public static class ReportingModuleExtensions
{
  public static IServiceCollection AddReportingModuleServices(this IServiceCollection services,
    ConfigurationManager config, ILogger logger, List<Assembly> mediatRAssemblies)
  {
    //string? connectionString = config.GetConnectionString("ReportingConnectionString");
    //services.AddDbContext<OrderProcessingDbContext>(options => options.UseSqlServer(connectionString));

    // Add User Services
    services.AddTransient<ITopSellingBooksReportService, TopSellingBooksReportService>();

    // If using MediatR in this module, add any assemblies that contain handlers
    mediatRAssemblies.Add(typeof(ReportingModuleExtensions).Assembly);

    logger.Information("{Module} module services registered", "Reporting");

    return services;
  }
}
