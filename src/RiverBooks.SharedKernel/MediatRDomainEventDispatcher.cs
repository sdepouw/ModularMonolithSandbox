using MediatR;

namespace RiverBooks.SharedKernel;

public class MediatRDomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
  public async Task DispatchAndClearEvents(IHaveDomainEvents[] entitiesWithEvents)
  {
    foreach (IHaveDomainEvents entity in entitiesWithEvents)
    {
      DomainEventBase[] events = entity.DomainEvents.ToArray();
      entity.ClearDomainEvents();
      foreach (DomainEventBase domainEvent in events)
      {
        await mediator.Publish(domainEvent).ConfigureAwait(false);
      }
    }
  }
}
