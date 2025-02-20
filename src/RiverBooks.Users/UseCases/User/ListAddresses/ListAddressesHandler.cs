using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;
using RiverBooks.Users.UserEndpoints;

namespace RiverBooks.Users.UseCases.User.ListAddresses;

internal class ListAddressesHandler(IApplicationUserRepository userRepository)
  : IRequestHandler<ListAddressesQuery, Result<List<UserAddressDTO>>>
{
  public async Task<Result<List<UserAddressDTO>>> Handle(ListAddressesQuery request, CancellationToken cancellationToken)
  {
    ApplicationUser? user = await userRepository.GetUserWithAddressesByEmailAsync(request.EmailAddress);
    if (user == null)
    {
      return Result.Unauthorized();
    }
    return user.Addresses
      .Select(a => new UserAddressDTO(a.Id, a.StreetAddress.Street1, a.StreetAddress.Street2, a.StreetAddress.City, a.StreetAddress.State, a.StreetAddress.PostalCode, a.StreetAddress.Country))
      .ToList();
  }
}
