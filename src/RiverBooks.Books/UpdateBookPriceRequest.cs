namespace RiverBooks.Books;

internal record UpdateBookPriceRequest(Guid Id, decimal NewPrice);
