using MediatR;

namespace RiverBooks.OrderProcessing.Contracts;

public record OrderCreatedIntegrationEvent(OrderDetailsDTO OrderDetails) : INotification
{
  public DateTimeOffset DateCreated { get; } = DateTimeOffset.UtcNow;
}
