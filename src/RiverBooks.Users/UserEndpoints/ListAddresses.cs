using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases.User.ListAddresses;

namespace RiverBooks.Users.UserEndpoints;

internal class ListAddresses(IMediator mediator) : EndpointWithoutRequest<AddressListResponse>
{
  public override void Configure()
  {
    Get("/users/addresses");
    Claims("EmailAddress");
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    string emailAddress = User.FindFirstValue("EmailAddress") ?? "";

    ListAddressesQuery query = new(emailAddress);

    Result<List<UserAddressDTO>> result = await mediator.Send(query, cancellationToken);

    if (result.IsUnauthorized())
    {
      await SendUnauthorizedAsync(cancellationToken);
    }
    else
    {
      await SendOkAsync(new AddressListResponse(result), cancellationToken);
    }
  }
}
