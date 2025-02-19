using Ardalis.Result;
using MediatR;
using RiverBooks.Users.CartEndpoints;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.UseCases.Cart.ListItems;

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
