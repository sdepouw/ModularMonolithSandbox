using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.EmailSending.Integrations;
using RiverBooks.Users.Contracts;

namespace RiverBooks.OrderProcessing.Domain;

internal class SendConfirmationEmailOrderCreatedEventHandler(IMediator mediator,
  ILogger<SendConfirmationEmailOrderCreatedEventHandler> logger): INotificationHandler<OrderCreatedEvent>
{
  public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
  {
    UserDetailsByIdQuery userQuery = new(notification.Order.UserId);
    Result<UserDetails> result = await mediator.Send(userQuery, cancellationToken);
    if (result.IsSuccess)
    {
      UserDetails user = result.Value;
      SendEmailCommand sendEmailCommand = new(user.EmailAddress, "donotreply@example.com", "RiverBooks Order Placed",
        $"Thanks for your money {user.FullName}! You got {notification.Order.OrderItems.Count} item(s).");
      await mediator.Send(sendEmailCommand, cancellationToken);
    }
    else
    {
      logger.LogError("[{Handler}] Could not send Order Placed Email to User {UserId} for Order {OrderId}: {Errors}",
        nameof(SendConfirmationEmailOrderCreatedEventHandler), notification.Order.UserId, notification.Order.Id, result.Errors);
    }
  }
}
