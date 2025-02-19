using Ardalis.GuardClauses;

namespace RiverBooks.OrderProcessing.Domain;

internal class OrderItem
{
  public Guid Id { get; private set; }
  public Guid BookId { get; private set; }
  public string Description { get; private set; } = "";
  public int Quantity { get; private set; }
  public decimal UnitPrice { get; private set; }

  public OrderItem(Guid bookId, string description, int quantity, decimal unitPrice)
  {
    BookId = Guard.Against.Default(bookId);
    Description = Guard.Against.NullOrWhiteSpace(description);
    Quantity = Guard.Against.Negative(quantity);
    UnitPrice = Guard.Against.Negative(unitPrice);
  }
}
