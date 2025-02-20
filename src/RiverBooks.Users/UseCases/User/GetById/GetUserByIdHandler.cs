using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.UseCases.User.GetById;

internal class GetUserByIdHandler(IApplicationUserRepository userRepo) : IRequestHandler<GetUserByIdQuery, Result<UserDTO>>
{
  public async Task<Result<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
  {
    ApplicationUser? user = await userRepo.GetUserByIdAsync(request.UserId);

    if (user?.Email is null) return Result.NotFound();

    return new UserDTO(Guid.Parse(user.Id), user.FullName, user.Email);
  }
}
