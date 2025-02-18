namespace RiverBooks.Users.CartEndpoints;

internal record AddItemRequest(Guid BookId, int Quantity);
