using FastEndpoints;

namespace RiverBooks.Reporting.ReportEndpoints;

internal class TopSalesByMonth2(ISalesReportService reportService)
  : Endpoint<TopSalesByMonthRequest, TopSalesByMonthResponse>
{
  public override void Configure()
  {
    Get("/topsales2");
    AllowAnonymous(); // TODO: lock down
  }

  public override async Task HandleAsync(TopSalesByMonthRequest request, CancellationToken cancellationToken)
  {
    TopBooksByMonthReport report = await reportService.GetTopBooksByMonthReportAsync(request.Month, request.Year);
    TopSalesByMonthResponse response = new(report);
    await SendAsync(response, cancellation: cancellationToken);
  }
}
