namespace RiverBooks.OrderProcessing.Domain;

internal class Order
{
  public Guid Id { get; private set; }
  public Guid UserId { get; private set; }
  public Address ShippingAddress { get; private set; } = null!;
  public Address BillingAddress { get; private set; } = null!;
  private readonly List<OrderItem> _orderItems = [];
  public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
  public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;

  private void AddOrderItem(OrderItem item) => _orderItems.Add(item);

  internal class Factory
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
      return order;
    }
  }
}
