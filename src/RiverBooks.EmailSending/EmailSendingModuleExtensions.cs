using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace RiverBooks.EmailSending;

public static class EmailSendingModuleExtensions
{
  public static IServiceCollection AddEmailSendingModuleServices(this IServiceCollection services, ConfigurationManager config,
    ILogger logger, List<Assembly> mediatRAssemblies)
  {
    // string? connectionString = config.GetConnectionString("EmailSendingConnectionString");

    services.AddScoped<ISendEmail, MimeKitEmailSender>();

    // If using MediatR in this module, add any assemblies that contain handlers
    mediatRAssemblies.Add(typeof(EmailSendingModuleExtensions).Assembly);

    logger.Information("{Module} module services registered", "Email Sending");
    return services;
  }
}
