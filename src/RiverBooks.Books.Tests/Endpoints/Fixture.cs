using FastEndpoints.Testing;
using RiverBooks.Web;

namespace RiverBooks.Books.Tests.Endpoints;

public class Fixture : AppFixture<Program>
{
  protected override ValueTask SetupAsync()
  {
    Client = CreateClient();
    return ValueTask.CompletedTask;
  }

  protected override ValueTask TearDownAsync()
  {
    Client.Dispose();
    return base.TearDownAsync();
  }
}
