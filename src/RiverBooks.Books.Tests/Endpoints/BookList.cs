using FastEndpoints;
using FastEndpoints.Testing;
using RiverBooks.Books.BookEndpoints;

namespace RiverBooks.Books.Tests.Endpoints;

public class BookList(Fixture fixture) : TestBase<Fixture>
{
  [Fact]
  public async Task ReturnsThreeBooksAsync()
  {
    TestResult<ListResponse> testResult = await fixture.Client.GETAsync<List, ListResponse>();

    testResult.Response.EnsureSuccessStatusCode();

    testResult.Result.Books.Count.ShouldBe(3);
  }
}
