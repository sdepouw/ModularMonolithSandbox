namespace RiverBooks.Reporting;

internal interface ITopSellingBooksReportService
{
  Task<TopBooksByMonthReport> ReachInSqlQuery(int month, int year);
}
