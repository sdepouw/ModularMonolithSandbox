using System.Reflection;
using FastEndpoints;
using FastEndpoints.Security;
using RiverBooks.Books;
using RiverBooks.EmailSending;
using RiverBooks.OrderProcessing;
using RiverBooks.Reporting;
using RiverBooks.SharedKernel;
using RiverBooks.Users;
using Serilog;
using ILogger = Serilog.ILogger;

ILogger logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("Starting web host");

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
builder.Services.AddOpenApi();
builder.Services.AddFastEndpoints()
  .AddAuthenticationJwtBearer(opts => opts.SigningKey = builder.Configuration["Auth:JwtSecret"] ?? "")
  // .AddJWTBearerAuth(builder.Configuration["Auth:JwtSecret"] ?? "") // Obsolete
  .AddAuthorization();

// Add Module Services
List<Assembly> mediatRAssemblies = [typeof(RiverBooks.Web.Program).Assembly];
builder.Services.AddBookModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddUserModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddOrderProcessingModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddEmailSendingModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddReportingModuleServices(builder.Configuration, logger, mediatRAssemblies);

// Set up MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray()));
builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
// The order matters! Logging is going to happen before fluent validation
builder.Services.AddMediatRLoggingBehavior();
builder.Services.AddMediatRFluentValidationBehavior();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseAuthentication().UseAuthorization();
app.UseFastEndpoints();

app.Run();

namespace RiverBooks.Web
{
  public class Program; // Needed for tests
}
