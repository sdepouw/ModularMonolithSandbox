using System.Globalization;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RiverBooks.Reporting;

internal class DefaultSalesReportService(IConfiguration config, ILogger<DefaultSalesReportService> logger)
  : ISalesReportService
{
  private readonly string _connString = config.GetConnectionString("ReportingConnectionString") ?? "";

  public async Task<TopBooksByMonthReport> GetTopBooksByMonthReportAsync(int month, int year)
  {
    const string sql =
      """
      SELECT BookId, Title, Author, UnitsSold as Units, TotalSales as Sales
      FROM Reporting.MonthlyBookSales
      WHERE Month = @month and Year = @year
      ORDER BY TotalSales DESC
      """;
    await using SqlConnection conn = new(_connString);
    logger.LogInformation("Executing query: {Sql}", sql);
    List<BookSalesResult> results = (await conn.QueryAsync<BookSalesResult>(sql, new { month, year }))
      .ToList();

    string monthName = CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(month);
    return new(year, month, monthName, results);
  }
}
