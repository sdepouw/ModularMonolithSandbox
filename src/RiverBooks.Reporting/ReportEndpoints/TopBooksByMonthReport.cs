namespace RiverBooks.Reporting.ReportEndpoints;

internal record TopBooksByMonthReport(int Year, int Month, string MonthName, List<BookSalesResult> Results);