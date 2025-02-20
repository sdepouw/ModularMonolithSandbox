using RiverBooks.SharedKernel;

namespace RiverBooks.OrderProcessing.Domain;

internal class OrderCreatedEvent(Order order) : DomainEventBase
{
  public Order Order { get; } = order;
}
