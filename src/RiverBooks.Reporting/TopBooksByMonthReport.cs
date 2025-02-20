namespace RiverBooks.Reporting;

internal record TopBooksByMonthReport(int Year, int Month, string MonthName, List<BookSalesResult> Results);