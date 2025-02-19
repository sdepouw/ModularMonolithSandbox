using MediatR;
using Microsoft.Extensions.Logging;

namespace RiverBooks.Users;

internal class LogNewAddressesHandler(ILogger<LogNewAddressesHandler> logger) : INotificationHandler<AddressAddedEvent>
{
  public Task Handle(AddressAddedEvent notification, CancellationToken cancellationToken)
  {
    logger.LogInformation("[DE Handler] New address added to user {User}: {Address}",
      notification.NewAddress.UserId, notification.NewAddress.StreetAddress);

    return Task.CompletedTask;
  }
}
