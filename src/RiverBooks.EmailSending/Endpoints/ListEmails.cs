using FastEndpoints;
using MongoDB.Driver;

namespace RiverBooks.EmailSending.Endpoints;

// For sake of simplicity, no DTOs for the Emails. Normally we would.
// We also are not using UseCases/Queries/Mediator/etc. for brevity.
internal class ListEmails(IMongoCollection<EmailOutboxEntity> emailCollection) : EndpointWithoutRequest<ListEmailResponse>
{
  public override void Configure()
  {
    Get("/emails");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    // TODO: Implement paging
    FilterDefinition<EmailOutboxEntity> filter = Builders<EmailOutboxEntity>.Filter.Empty;
    List<EmailOutboxEntity> emailEntities = await emailCollection.Find(filter).ToListAsync(cancellationToken);

    Response = new ListEmailResponse(emailEntities.Count, emailEntities);
  }
}
