using Ardalis.Result;
using MediatR;
using RiverBooks.Users.CartEndpoints;

namespace RiverBooks.Users.UseCases;

internal class ListCartItemsHandler(IApplicationUserRepository userRepository)
  : IRequestHandler<ListCartItemsQuery, Result<List<CartItemDTO>>>
{
  public async Task<Result<List<CartItemDTO>>> Handle(ListCartItemsQuery request, CancellationToken cancellationToken)
  {
    ApplicationUser? user = await userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);
    if (user is null)
    {
      return Result.Unauthorized();
    }

    return user.CartItems
      .Select(item => new CartItemDTO(item.Id, item.BookId, item.Description, item.Quantity, item.UnitPrice))
      .ToList();
  }
}