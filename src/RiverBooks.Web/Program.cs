using FastEndpoints;
using RiverBooks.Books;
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
builder.Services.AddFastEndpoints();

// Add Module Services
builder.Services.AddBookModuleServices(builder.Configuration, logger);
builder.Services.AddUserModuleServices(builder.Configuration, logger);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseFastEndpoints();

app.Run();

public partial class Program; // Needed for tests
