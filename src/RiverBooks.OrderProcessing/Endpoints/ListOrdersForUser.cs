using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;

namespace RiverBooks.OrderProcessing.Endpoints;

internal class ListOrdersForUser(IMediator mediator) : EndpointWithoutRequest<ListOrdersForUserResponse>
{
  public override void Configure()
  {
    Get("/orders");
    Claims("EmailAddress");
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    string emailAddress = User.FindFirstValue("EmailAddress") ?? "";

    ListOrdersForUserQuery query = new(emailAddress);
    Result<List<OrderSummary>> result = await mediator.Send(query, cancellationToken);

    if (result.IsUnauthorized())
    {
      await SendUnauthorizedAsync(cancellationToken);
    }
    else
    {
      ListOrdersForUserResponse response = new(result.Value);
      await SendAsync(response, cancellation: cancellationToken);
    }
  }
}
