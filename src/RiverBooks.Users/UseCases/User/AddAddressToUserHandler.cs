using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.UseCases.User;

internal class AddAddressToUserHandler(IApplicationUserRepository userRepository, ILogger<AddAddressToUserHandler> logger)
  : IRequestHandler<AddAddressToUserCommand, Result>
{
  public async Task<Result> Handle(AddAddressToUserCommand request, CancellationToken cancellationToken)
  {
    ApplicationUser? user = await userRepository.GetUserWithAddressesByEmailAsync(request.EmailAddress);

    if (user is null)
    {
      return Result.Unauthorized();
    }

    Address addressToAdd = new(request.Street1, request.Street2, request.City, request.State, request.PostalCode, request.Country);
    UserStreetAddress userAddress = user.AddAddress(addressToAdd);
    await userRepository.SaveChangesAsync();

    logger.LogInformation("[UseCase] Added address {Address} to user {Email} (Total: {AddressCount})",
      userAddress.StreetAddress, request.EmailAddress, user.Addresses.Count);

    return Result.Success();
  }
}
