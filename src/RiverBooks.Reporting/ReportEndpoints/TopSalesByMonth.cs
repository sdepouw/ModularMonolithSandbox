using FastEndpoints;

namespace RiverBooks.Reporting.ReportEndpoints;

internal class TopSalesByMonth : Endpoint<TopSalesByMonthRequest, TopSalesByMonthResponse>
{
  public override void Configure()
  {
    Get("/topsales");
    AllowAnonymous(); // TODO: lock down
  }

  public override async Task HandleAsync(TopSalesByMonthRequest request, CancellationToken cancellationToken)
  {
    TopSalesByMonthResponse response = new() { };
    await SendAsync(response, cancellation: cancellationToken);
  }
}

internal record TopSalesByMonthRequest(int Year, int Month);
internal record TopSalesByMonthResponse();
