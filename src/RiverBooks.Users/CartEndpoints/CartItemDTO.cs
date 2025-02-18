namespace RiverBooks.Users.CartEndpoints;

internal record CartItemDTO(Guid Id, Guid BookId, string Description, int Quantity, decimal UnitPrice);
