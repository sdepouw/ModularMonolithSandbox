using System.Globalization;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RiverBooks.Reporting;

internal class TopSellingBooksReportService(ILogger<TopSellingBooksReportService> logger, IConfiguration config)
  : ITopSellingBooksReportService
{
  private readonly string _connectionString = config.GetConnectionString("ReportingConnectionString") ?? "";

  public async Task<TopBooksByMonthReport> ReachInSqlQuery(int month, int year)
  {
    const string sql =
      """
      SELECT b.Id AS BookId, b.Title, b.Author, sum(oi.Quantity) AS Units, sum(oi.UnitPrice * oi.Quantity) AS Sales
      FROM Books.Books b
      INNER JOIN OrderProcessing.OrderItem oi ON oi.BookId = b.Id
      INNER JOIN OrderProcessing.Orders o ON oi.OrderId = o.Id
      WHERE MONTH(o.DateCreated) = @month
      AND YEAR(o.DateCreated) = @year
      GROUP BY b.Id, b.Title, b.Author
      ORDER BY Sales Desc
      """;

    await using SqlConnection connection = new SqlConnection(_connectionString);
    logger.LogInformation("Executing query: {Query}", sql);
    List<BookSalesResult> results = (await connection.QueryAsync<BookSalesResult>(sql, new { month, year }))
      .ToList();

    string monthName = CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(month);
    return new TopBooksByMonthReport(year, month, monthName, results);
  }
}
