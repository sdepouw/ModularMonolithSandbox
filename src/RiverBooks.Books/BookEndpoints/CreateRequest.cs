namespace RiverBooks.Books.BookEndpoints;

internal record CreateRequest(Guid? Id, string Title, string Author, decimal Price);
