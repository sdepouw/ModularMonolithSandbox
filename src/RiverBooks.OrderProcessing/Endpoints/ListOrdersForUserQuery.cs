using Ardalis.Result;
using MediatR;

namespace RiverBooks.OrderProcessing.Endpoints;

internal record ListOrdersForUserQuery(string EmailAddress) : IRequest<Result<List<OrderSummary>>>;

internal class ListOrdersForUserQueryHandler(IOrderRepository orderRepository)
  : IRequestHandler<ListOrdersForUserQuery, Result<List<OrderSummary>>>
{
  public async Task<Result<List<OrderSummary>>> Handle(ListOrdersForUserQuery request, CancellationToken cancellationToken)
  {
    // TODO: Look up UserId for EmailAddress and filter by User
    List<Order> orders = await orderRepository.ListAsync();

    return orders
      .Select(o => new OrderSummary(o.Id, o.UserId, o.DateCreated, null, o.OrderItems.Sum(oi => oi.UnitPrice)))
      .ToList();
  }
}
