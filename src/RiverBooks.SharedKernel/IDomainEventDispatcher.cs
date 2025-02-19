namespace RiverBooks.SharedKernel;

public interface IDomainEventDispatcher
{
  Task DispatchAndClearEvents(IHaveDomainEvents[] entitiesWithEvents);
}
