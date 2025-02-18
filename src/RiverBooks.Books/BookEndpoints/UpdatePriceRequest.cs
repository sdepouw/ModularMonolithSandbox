namespace RiverBooks.Books.BookEndpoints;

internal record UpdatePriceRequest(Guid Id, decimal NewPrice);
