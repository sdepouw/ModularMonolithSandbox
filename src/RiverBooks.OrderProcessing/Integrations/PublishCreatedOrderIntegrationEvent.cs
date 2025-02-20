using MediatR;
using RiverBooks.OrderProcessing.Contracts;
using RiverBooks.OrderProcessing.Domain;

namespace RiverBooks.OrderProcessing.Integrations;

internal class PublishCreatedOrderIntegrationEvent(IMediator mediator) : INotificationHandler<OrderCreatedEvent>
{
  public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
  {
    List<OrderItemDetails> orderItems = notification.Order.OrderItems
      .Select(oi => new OrderItemDetails(oi.BookId, oi.Quantity, oi.UnitPrice, oi.Description))
      .ToList();
    OrderDetailsDTO orderDetails = new(notification.Order.DateCreated, notification.Order.Id,
      notification.Order.UserId, orderItems);
    OrderCreatedIntegrationEvent integrationEvent = new(orderDetails);
    await mediator.Publish(integrationEvent, cancellationToken);
  }
}
