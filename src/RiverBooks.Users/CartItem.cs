using Ardalis.GuardClauses;

namespace RiverBooks.Users;

public class CartItem
{
  public Guid Id { get; private set; }
  public Guid BookId { get; private set; }
  public string Description { get; private set; }
  public int Quantity { get; private set; }
  public decimal UnitPrice { get; private set; }

  // Older versions of EF needed to have a blank constructor, and be configured so that the Id property is
  // set to "ValueGeneratedNever." I didn't run into that error here in .NET 9, however, and everything
  // seems to work.
  public CartItem(Guid bookId, string description, int quantity, decimal unitPrice)
  {
    BookId = Guard.Against.Default(bookId);
    Description = Guard.Against.NullOrWhiteSpace(description);
    Quantity = Guard.Against.Negative(quantity);
    UnitPrice = Guard.Against.Negative(unitPrice);
  }

  internal void AdjustQuantity(int quantity) => Quantity = Guard.Against.Negative(quantity);
}
