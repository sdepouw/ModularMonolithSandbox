using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users.UseCases;

internal class AddItemToCartHandler(IApplicationUserRepository userRepository) : IRequestHandler<AddItemToCartCommand, Result>
{
  public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
  {
    ApplicationUser? user = await userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);
    if (user is null)
    {
      return Result.Unauthorized();
    }

    // TODO: Get description and price form Books Module
    CartItem newItem = new(request.BookId, "description", request.Quantity, 1.00m);
    user.AddItemToCart(newItem);

    await userRepository.SaveChangesAsync();

    return Result.Success();
  }
}
