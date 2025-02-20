namespace RiverBooks.Reporting.ReportEndpoints;

internal record BookSalesResult(Guid Id, string Title, string Author, int Units, decimal Sales);
