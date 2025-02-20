namespace RiverBooks.Reporting.Integrations;

internal record BookSale(string Title, string Author, Guid BookId, int Month, int Year, int UnitsSold, decimal TotalSales);
