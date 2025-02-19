namespace RiverBooks.Users;

public interface IDomainEventDispatcher
{
  Task DispatchAndClearEvents(IHaveDomainEvents[] entitiesWithEvents);
}
