using Ardalis.Result;
using MongoDB.Driver;

namespace RiverBooks.EmailSending;

internal class MongoDbOutboxService(IMongoCollection<EmailOutboxEntity> emailCollection) : IOutboxService
{
  public Task QueueEmailForSending(EmailOutboxEntity entity) => emailCollection.InsertOneAsync(entity);
  public async Task<Result<EmailOutboxEntity>> GetUnprocessedEmailEntity()
  {
    FilterDefinition<EmailOutboxEntity> unprocessedFilter = Builders<EmailOutboxEntity>.Filter
      .Eq(entity => entity.DateTimeUtcProcessed, null);
    EmailOutboxEntity? unsentEmailEntity = await emailCollection.Find(unprocessedFilter).FirstOrDefaultAsync();

    if (unsentEmailEntity is null) return Result.NotFound();

    return unsentEmailEntity;
  }
}
