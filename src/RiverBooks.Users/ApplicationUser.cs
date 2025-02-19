using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace RiverBooks.Users;

internal class ApplicationUser : IdentityUser
{
  public string FullName { get; set; } = "";

  private readonly List<CartItem> _cartItems = [];
  public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();
  private readonly List<UserStreetAddress> _addresses = [];
  public IReadOnlyCollection<UserStreetAddress> Addresses => _addresses.AsReadOnly();

  public void AddItemToCart(CartItem item)
  {
    Guard.Against.Null(item);
    CartItem? existingBook = _cartItems.SingleOrDefault(x => x.BookId == item.BookId);
    if (existingBook != null)
    {
      existingBook.AdjustQuantity(existingBook.Quantity + item.Quantity);
      existingBook.UpdateDescription(item.Description);
      existingBook.UpdatePrice(item.UnitPrice);
      return;
    }
    _cartItems.Add(item);
  }

  internal void ClearCart() => _cartItems.Clear();

  internal UserStreetAddress AddAddress(Address address)
  {
    Guard.Against.Null(address);

    UserStreetAddress? existingAddress = _addresses.SingleOrDefault(a => a.StreetAddress == address);
    if (existingAddress is not null)
    {
      return existingAddress;
    }

    UserStreetAddress newAddress = new(Id, address);
    _addresses.Add(newAddress);

    return newAddress;
  }
}
