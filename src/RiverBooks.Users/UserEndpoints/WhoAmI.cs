using System.Security.Claims;
using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace RiverBooks.Users.UserEndpoints;

internal class WhoAmI(ILogger<WhoAmI> logger) : EndpointWithoutRequest
{
  public override void Configure()
  {
    Get("/whoami");
    Claims("EmailAddress");
  }

  public override Task HandleAsync(CancellationToken cancellationToken)
  {
    string emailAddress = User.FindFirstValue("EmailAddress") ?? "";
    logger.LogInformation("[WhoAmI] Email Claim is {Email}", emailAddress);
    return SendAsync(emailAddress, cancellation: cancellationToken);
  }
}
