using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.OrderProcessing.Contracts;

namespace RiverBooks.OrderProcessing.Integrations;

internal class CreateOrderCommandHandler(IOrderRepository orderRepository, ILogger<CreateOrderCommandHandler> logger)
  : IRequestHandler<CreateOrderCommand, Result<OrderDetailsResponse>>
{
  public async Task<Result<OrderDetailsResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
  {
    IEnumerable<OrderItem> orderItems = request.OrderItems
      .Select(oi => new OrderItem(oi.BookId, oi.Description, oi.Quantity, oi.UnitPrice));

    Address shippingAddress = new("123 Fake St.", "", "Columbus", "OH", "43230", "USA");
    Address billingAddress = new("456 Pretend Ave.", "Apt 20", "Beverly Hills", "CA", "90210", "USA");
    Order newOrder = Order.Factory.Create(request.UserId, shippingAddress, billingAddress, orderItems);

    await orderRepository.AddAsync(newOrder);
    await orderRepository.SaveChangesAsync();

    logger.LogInformation("New Order Created! {NewOrderId}", newOrder.Id);

    return new OrderDetailsResponse(newOrder.Id);
  }
}
