using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.Books.Contracts;
using RiverBooks.OrderProcessing.Contracts;

namespace RiverBooks.Reporting.Integrations;

internal class NewOrderCreatedIngestionHandler(ILogger<NewOrderCreatedIngestionHandler> logger,
  OrderIngestionService orderIngestionService, IMediator mediator)
  : INotificationHandler<OrderCreatedIntegrationEvent>
{
  public async Task Handle(OrderCreatedIntegrationEvent notification, CancellationToken cancellationToken)
  {
    logger.LogInformation("Handling order created event to populate reporting database");

    List<OrderItemDetails> orderItems = notification.OrderDetails.OrderItems;
    int year = notification.OrderDetails.DateCreated.Year;
    int month = notification.OrderDetails.DateCreated.Month;

    foreach (OrderItemDetails item in orderItems)
    {
      // Look up book details to get author and title
      // TODO: Implement Materialized View or other cache
      BookDetailsQuery query = new(item.BookId);
      Result<BookDetailsResponse> result = await mediator.Send(query, cancellationToken);

      if (!result.IsSuccess)
      {
        logger.LogWarning("Issue loading book details for {Id}", item.BookId);
        continue;
      }

      string author = result.Value.Author;
      string title = result.Value.Title;

      BookSale sale = new(title, author, item.BookId, month, year, item.Quantity, item.Quantity * item.UnitPrice);
      await orderIngestionService.AddOrUpdateMonthlyBookSalesAsync(sale);
    }
  }
}
