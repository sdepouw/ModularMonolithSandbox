using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases.Cart.ListItems;

namespace RiverBooks.Users.CartEndpoints;

internal class ListItems(IMediator mediator) : EndpointWithoutRequest<CartResponse>
{
  public override void Configure()
  {
    Get("/cart");
    Claims("EmailAddress");
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    string emailAddress = User.FindFirstValue("EmailAddress") ?? "";

    ListCartItemsQuery query = new ListCartItemsQuery(emailAddress);
    Result<List<CartItemDTO>> result = await mediator.Send(query, cancellationToken);

    if (result.IsUnauthorized())
    {
      await SendUnauthorizedAsync(cancellationToken);
      return;
    }

    await SendAsync(new(result), cancellation: cancellationToken);
  }
}
