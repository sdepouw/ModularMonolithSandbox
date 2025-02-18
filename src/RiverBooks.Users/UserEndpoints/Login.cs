using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;

namespace RiverBooks.Users.UserEndpoints;

internal class Login(UserManager<ApplicationUser> userManager) : Endpoint<UserLoginRequest>
{
  public override void Configure()
  {
    Post("/users/login");
    AllowAnonymous();
  }

  public override async Task HandleAsync(UserLoginRequest request, CancellationToken cancellationToken)
  {
    ApplicationUser? user = await userManager.FindByEmailAsync(request.Email);
    if (user is null)
    {
      await SendUnauthorizedAsync(cancellationToken);
      return;
    }

    bool loginSuccessful = await userManager.CheckPasswordAsync(user, request.Password);
    if (!loginSuccessful)
    {
      await SendUnauthorizedAsync(cancellationToken);
      return;
    }

    string jwtSecret = Config["Auth:JwtSecret"]!;

    // Obsolete
    // string token = JWTBearer.CreateToken(jwtSecret, p => p["EmailAddress"] = user.Email ?? "");
    string token = JwtBearer.CreateToken(opts =>
    {
      opts.SigningKey = jwtSecret;
      opts.User["EmailAddress"] = user.Email ?? "";
    });
    await SendAsync(token, cancellation: cancellationToken);
  }
}
