using Ardalis.Result;
using MediatR;
using RiverBooks.Books.Contracts;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.UseCases.Cart.AddItem;

internal class AddItemToCartHandler(IApplicationUserRepository userRepository, IMediator mediator)
  : IRequestHandler<AddItemToCartCommand, Result>
{
  public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
  {
    ApplicationUser? user = await userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);
    if (user is null)
    {
      return Result.Unauthorized();
    }

    BookDetailsQuery query = new(request.BookId);
    Result<BookDetailsResponse> bookDetailsResult = await mediator.Send(query, cancellationToken);
    if (bookDetailsResult.IsNotFound())
    {
      return Result.NotFound();
    }
    BookDetailsResponse bookDetails = bookDetailsResult.Value;
    string description = $"{bookDetails.Title} by {bookDetails.Author}";
    CartItem newItem = new(request.BookId, description, request.Quantity, bookDetails.Price);
    user.AddItemToCart(newItem);

    await userRepository.SaveChangesAsync();

    return Result.Success();
  }
}
