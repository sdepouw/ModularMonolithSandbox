using System.ComponentModel.DataAnnotations.Schema;
using RiverBooks.SharedKernel;

namespace RiverBooks.OrderProcessing.Domain;

internal class Order : IHaveDomainEvents
{
  public Guid Id { get; private set; }
  public Guid UserId { get; private set; }
  public Address ShippingAddress { get; private set; } = null!;
  public Address BillingAddress { get; private set; } = null!;
  private readonly List<OrderItem> _orderItems = [];
  public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
  public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;

  private void AddOrderItem(OrderItem item) => _orderItems.Add(item);

  private readonly List<DomainEventBase> _domainEvents = [];
  [NotMapped]
  public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();
  void IHaveDomainEvents.ClearDomainEvents() => _domainEvents.Clear();
  private void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);

  internal static class Factory
  {
    public static Order Create(Guid userId, Address shipping, Address billing, IEnumerable<OrderItem> orderItems)
    {
      Order order = new()
      {
        UserId = userId,
        ShippingAddress = shipping,
        BillingAddress = billing,
      };
      foreach (OrderItem orderItem in orderItems)
      {
        order.AddOrderItem(orderItem);
      }
      order.RegisterDomainEvent(new OrderCreatedEvent(order));
      return order;
    }
  }
}
