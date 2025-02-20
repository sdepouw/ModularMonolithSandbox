using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RiverBooks.EmailSending.Integrations;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.UseCases.User.Create;

internal class CreateUserCommandHandler(UserManager<ApplicationUser> userManager, IMediator mediator)
  : IRequestHandler<CreateUserCommand, Result>
{
  public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    ApplicationUser newUser = new()
    {
      Email = request.Email,
      UserName = request.Email
    };
    IdentityResult result = await userManager.CreateAsync(newUser, request.Password);

    if (!result.Succeeded)
    {
      return Result.Error(new ErrorList(result.Errors.Select(e => e.Description)));
    }

    // TODO: Fetch from config
    SendEmailCommand sendEmailCommand = new(request.Email, "donotreply@example.com", "Welcome to RiverBooks", "This is a website!");
    await mediator.Send(sendEmailCommand, cancellationToken);
    return Result.Success();
  }
}
