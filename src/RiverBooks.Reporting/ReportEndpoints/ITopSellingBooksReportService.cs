namespace RiverBooks.Reporting.ReportEndpoints;

internal interface ITopSellingBooksReportService
{
  Task<TopBooksByMonthReport> ReachInSqlQuery(int month, int year);
}
