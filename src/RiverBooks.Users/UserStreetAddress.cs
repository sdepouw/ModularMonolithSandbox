using Ardalis.GuardClauses;

namespace RiverBooks.Users;

internal class UserStreetAddress
{
  public Guid Id { get; private set; } = Guid.NewGuid();
  public string UserId { get; private set; } = string.Empty;
  public Address StreetAddress { get; private set; } = null!;

  // ReSharper disable once UnusedMember.Local (required for EF)
  private UserStreetAddress() { }

  public UserStreetAddress(string userId, Address streetAddress)
  {
    UserId = Guard.Against.NullOrWhiteSpace(userId);
    StreetAddress = Guard.Against.Null(streetAddress);
  }
}
