using FastEndpoints;
using RiverBooks.Books;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddBookServices(builder.Configuration);
builder.Services.AddFastEndpoints();

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
