using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases.Cart.Checkout;

namespace RiverBooks.Users.CartEndpoints;

internal class Checkout(IMediator mediator) : Endpoint<CheckoutRequest, CheckoutResponse>
{
  public override void Configure()
  {
    Post("/cart/checkout");
    Claims("EmailAddress");
  }

  public override async Task HandleAsync(CheckoutRequest request, CancellationToken cancellationToken)
  {
    string emailAddress = User.FindFirstValue("EmailAddress") ?? "";

    CheckoutCartCommand command = new(emailAddress, request.ShippingAddressId, request.BillingAddressId);

    Result<Guid> result = await mediator.Send(command, cancellationToken);

    if (result.IsUnauthorized())
    {
      await SendUnauthorizedAsync(cancellationToken);
    }
    else
    {
      await SendOkAsync(new CheckoutResponse(result.Value), cancellationToken);
    }
  }
}
