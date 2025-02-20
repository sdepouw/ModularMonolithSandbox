using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Contracts;
using RiverBooks.Users.UseCases.User.GetById;

namespace RiverBooks.Users.Integrations;

internal class UserDetailsByIdHandler(IMediator mediator)
  : IRequestHandler<UserDetailsByIdQuery, Result<UserDetails>>
{
  public async Task<Result<UserDetails>> Handle(UserDetailsByIdQuery request, CancellationToken cancellationToken)
  {
    Result<UserDTO> result = await mediator.Send(new GetUserByIdQuery(request.UserId), cancellationToken);
    return result.IsSuccess
      ? result.Map(x => new UserDetails(x.UserId, x.FullName, x.EmailAddress))
      : Result.NotFound();
  }
}
