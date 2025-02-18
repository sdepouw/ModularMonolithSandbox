namespace RiverBooks.Books;

internal record UpdatePriceRequest(Guid Id, decimal NewPrice);