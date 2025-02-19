using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.OrderProcessing.Infrastructure;
using RiverBooks.OrderProcessing.Interfaces;
using RiverBooks.Users.Contracts;

namespace RiverBooks.OrderProcessing.Integrations;

internal class AddressCacheUpdatingNewUserAddressHandler(IOrderAddressCache addressCache,
  ILogger<AddressCacheUpdatingNewUserAddressHandler> logger)
  : INotificationHandler<NewUserAddressAddedIntegrationEvent>
{
  public async Task Handle(NewUserAddressAddedIntegrationEvent notification, CancellationToken cancellationToken)
  {
    Address address = new(notification.Details.Street1, notification.Details.Street2, notification.Details.City,
      notification.Details.State, notification.Details.PostalCode, notification.Details.Country);
    OrderAddress orderAddress = new(notification.Details.AddressId, address);

    await addressCache.StoreAsync(orderAddress);

    logger.LogInformation("Cache updated with new address {Address}", orderAddress);
  }
}
