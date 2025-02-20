using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases.User.Create;

namespace RiverBooks.Users.UserEndpoints;

internal class Create(IMediator mediator) : Endpoint<CreateRequest>
{
  public override void Configure()
  {
    Post("/users");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateRequest request, CancellationToken cancellationToken)
  {
    Result result = await mediator.Send(new CreateUserCommand(request.Email, request.Password), cancellationToken);
    if (!result.IsSuccess)
    {
      await SendResultAsync(result.ToMinimalApiResult());
      return;
    }
    await SendOkAsync(cancellationToken);
  }
}
