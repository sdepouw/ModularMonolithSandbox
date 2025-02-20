using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.UseCases.User.Create;

internal class CreateUserCommandHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<CreateUserCommand, Result>
{
  public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    ApplicationUser newUser = new()
    {
      Email = request.Email,
      UserName = request.Email
    };
    IdentityResult result = await userManager.CreateAsync(newUser, request.Password);

    return result.Succeeded
      ? Result.Success()
      : Result.Error(new ErrorList(result.Errors.Select(e => e.Description)));
  }
}
