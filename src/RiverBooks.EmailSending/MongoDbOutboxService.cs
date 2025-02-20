using MongoDB.Driver;

namespace RiverBooks.EmailSending;

internal class MongoDbOutboxService(IMongoCollection<EmailOutboxEntity> emailCollection) : IOutboxService
{
  public Task QueueEmailForSending(EmailOutboxEntity entity) => emailCollection.InsertOneAsync(entity);
}
