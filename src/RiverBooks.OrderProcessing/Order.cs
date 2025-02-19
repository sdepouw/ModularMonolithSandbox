namespace RiverBooks.OrderProcessing;

internal class Order
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public Address ShippingAddress { get; set; } = null!;
  public Address BillingAddress { get; set; } = null!;
  private readonly List<OrderItem> _orderItems = [];
  public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
  public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;

  private void AddOrderItem(OrderItem item) => _orderItems.Add(item);
}
