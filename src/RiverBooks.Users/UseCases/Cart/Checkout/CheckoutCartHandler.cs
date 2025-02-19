using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Contracts;

namespace RiverBooks.Users.UseCases.Cart.Checkout;

internal class CheckoutCartHandler(IMediator mediator, IApplicationUserRepository userRepository)
  : IRequestHandler<CheckoutCartCommand, Result<Guid>>
{
  public async Task<Result<Guid>> Handle(CheckoutCartCommand request, CancellationToken cancellationToken)
  {
    ApplicationUser? user = await userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

    if (user is null)
    {
      return Result.Unauthorized();
    }

    IEnumerable<OrderItemDetails> items = user.CartItems.Select(item => new OrderItemDetails(item.BookId, item.Quantity, item.UnitPrice, item.Description));

    CreateOrderCommand createOrderCommand = new(Guid.Parse(user.Id), request.ShippingAddressId, request.BillingAddressId, items.ToList());

    // TODO: Consider replacing with a message-based approached for perf reasons
    Result<OrderDetailsResponse> result = await mediator.Send(createOrderCommand, cancellationToken);

    if (!result.IsSuccess)
    {
      // Change from Result<OrderDetailsResponse> to Result<Guid>
      return result.Map(x => x.OrderId);
    }
    user.ClearCart();
    await userRepository.SaveChangesAsync();

    return Result.Success(result.Value.OrderId);
  }
}
