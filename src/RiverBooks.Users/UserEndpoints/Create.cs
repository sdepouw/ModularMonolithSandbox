using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace RiverBooks.Users.UserEndpoints;

public class Create(UserManager<ApplicationUser> userManager) : Endpoint<CreateRequest>
{
  public override void Configure()
  {
    Post("/users");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateRequest request, CancellationToken cancellationToken)
  {
    ApplicationUser newUser = new()
    {
      Email = request.Email,
      UserName = request.Email
    };
    await userManager.CreateAsync(newUser, request.Password);
    await SendOkAsync(cancellationToken);
  }
}
