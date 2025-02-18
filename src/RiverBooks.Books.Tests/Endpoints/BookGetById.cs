using FastEndpoints;
using FastEndpoints.Testing;
using RiverBooks.Books.BookEndpoints;

namespace RiverBooks.Books.Tests.Endpoints;

public class BookGetById(Fixture fixture) : TestBase<Fixture>
{
  [Theory]
  [InlineData("1d665949-a89d-4a0a-aff9-6c8fee8be2b9", "The Fellowship of the Ring")]
  [InlineData("d0c59d6e-9df6-4dc8-a443-500b91abbc31", "The Two Towers")]
  [InlineData("c305241e-5737-43a8-af67-6bd43be9bef7", "The Return of the King")]
  public async Task ReturnsExpectedBookGivenId(string validId, string expectedTitle)
  {
    Guid id = Guid.Parse(validId);
    GetByIdRequest request = new(id);

    TestResult<BookDTO> testResult = await fixture.Client.GETAsync<GetById, GetByIdRequest, BookDTO>(request);

    testResult.Response.EnsureSuccessStatusCode();
    testResult.Result.Title.ShouldBe(expectedTitle);
  }
}
