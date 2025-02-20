using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Serilog;

namespace RiverBooks.EmailSending;

public static class EmailSendingModuleExtensions
{
  public static IServiceCollection AddEmailSendingModuleServices(this IServiceCollection services, ConfigurationManager config,
    ILogger logger, List<Assembly> mediatRAssemblies)
  {
    // Configure MongoDB
    services.Configure<MongoDBSettings>(config.GetSection("MongoDB"));
    services.AddMongoDB(config);

    // Add module services
    services.AddTransient<ISendEmail, MimeKitEmailSender>();
    services.AddTransient<IOutboxService, MongoDbOutboxService>();
    services.AddTransient<ISendEmailsFromOutboxService, SendEmailsFromOutboxService>();

    // If using MediatR in this module, add any assemblies that contain handlers
    mediatRAssemblies.Add(typeof(EmailSendingModuleExtensions).Assembly);

    // Add BackgroundWorker
    services.AddHostedService<EmailSendingBackgroundService>();

    logger.Information("{Module} module services registered", "Email Sending");
    return services;
  }

  private static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration configuration)
  {
    // Register the MongoDB client as a singleton
    services.AddSingleton<IMongoClient>(_ =>
    {
      MongoDBSettings settings = configuration.GetSection("MongoDB").Get<MongoDBSettings>()!;
      BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
      return new MongoClient(settings.ConnectionString);
    });

    // Register the MongoDB database as a singleton
    services.AddSingleton(serviceProvider =>
    {
      MongoDBSettings settings = configuration.GetSection("MongoDB").Get<MongoDBSettings>()!;
      IMongoClient client = serviceProvider.GetRequiredService<IMongoClient>();
      return client.GetDatabase(settings.DatabaseName);
    });

    // Optionally, register specific collections here as scoped or singleton services
    // Example for a 'EmailOutboxEntity' collection
    services.AddTransient(serviceProvider =>
    {
      IMongoDatabase database = serviceProvider.GetRequiredService<IMongoDatabase>();
      return database.GetCollection<EmailOutboxEntity>("EmailOutboxEntityCollection");
    });

    return services;
  }
}
