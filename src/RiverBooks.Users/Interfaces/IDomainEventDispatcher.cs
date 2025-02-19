namespace RiverBooks.Users.Interfaces;

public interface IDomainEventDispatcher
{
  Task DispatchAndClearEvents(IHaveDomainEvents[] entitiesWithEvents);
}
