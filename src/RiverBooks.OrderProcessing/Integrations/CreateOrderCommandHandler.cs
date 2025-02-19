using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.OrderProcessing.Contracts;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.OrderProcessing.Infrastructure;
using RiverBooks.OrderProcessing.Interfaces;

namespace RiverBooks.OrderProcessing.Integrations;

internal class CreateOrderCommandHandler(IOrderRepository orderRepository, ILogger<CreateOrderCommandHandler> logger,
  IOrderAddressCache orderAddressCache) : IRequestHandler<CreateOrderCommand, Result<OrderDetailsResponse>>
{
  public async Task<Result<OrderDetailsResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
  {
    IEnumerable<OrderItem> orderItems = request.OrderItems
      .Select(oi => new OrderItem(oi.BookId, oi.Description, oi.Quantity, oi.UnitPrice));

    Result<OrderAddress> shippingAddress = await orderAddressCache.GetByIdAsync(request.ShippingAddressId);
    Result<OrderAddress> billingAddress = await orderAddressCache.GetByIdAsync(request.BillingAddressId);
    Order newOrder = Order.Factory.Create(request.UserId, shippingAddress.Value.Address, billingAddress.Value.Address, orderItems);

    await orderRepository.AddAsync(newOrder);
    await orderRepository.SaveChangesAsync();

    logger.LogInformation("New Order Created! {NewOrderId}", newOrder.Id);

    return new OrderDetailsResponse(newOrder.Id);
  }
}
