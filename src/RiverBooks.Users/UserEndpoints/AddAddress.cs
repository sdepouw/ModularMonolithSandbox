using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases.User.AddAddress;

namespace RiverBooks.Users.UserEndpoints;

internal sealed class AddAddress(IMediator mediator) : Endpoint<AddAddressRequest>
{
  public override void Configure()
  {
    Post("/users/addresses");
    Claims("EmailAddress");
  }

  public override async Task HandleAsync(AddAddressRequest request, CancellationToken cancellationToken)
  {
    string emailAddress = User.FindFirstValue("EmailAddress") ?? "";

    AddAddressToUserCommand command = new(emailAddress, request.Street1, request.Street2, request.City, request.State,
      request.PostalCode, request.Country);

    Result result = await mediator.Send(command, cancellationToken);

    if (result.IsUnauthorized())
    {
      await SendUnauthorizedAsync(cancellationToken);
    }
    else
    {
      await SendOkAsync(cancellationToken);
    }
  }
}
