using System.Security.Claims;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases.Cart.AddItem;

namespace RiverBooks.Users.CartEndpoints;

internal class AddItem(IMediator mediator) : Endpoint<AddItemRequest>
{
  public override void Configure()
  {
    Post("/cart");
    Claims("EmailAddress");
  }

  public override async Task HandleAsync(AddItemRequest request, CancellationToken cancellationToken)
  {
    string emailAddress = User.FindFirstValue("EmailAddress") ?? "";

    AddItemToCartCommand command = new(request.BookId, request.Quantity, emailAddress);
    Result result = await mediator.Send(command, cancellationToken);

    if (result.IsUnauthorized())
    {
      await SendUnauthorizedAsync(cancellationToken);
    }
    else if (result.IsInvalid())
    {
      await SendResultAsync(result.ToMinimalApiResult());
    }
    else
    {
      await SendOkAsync(cancellationToken);
    }
  }
}
