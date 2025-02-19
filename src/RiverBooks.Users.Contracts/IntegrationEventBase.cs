using MediatR;

namespace RiverBooks.Users.Contracts;

// TODO: Move elsewhere.
public abstract record IntegrationEventBase : INotification
{
  public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
}
