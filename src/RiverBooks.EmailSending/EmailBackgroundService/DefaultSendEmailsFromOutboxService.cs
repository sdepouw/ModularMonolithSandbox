using Ardalis.Result;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace RiverBooks.EmailSending.EmailBackgroundService;

internal interface IGetEmailsFromOutboxService
{
  Task<Result<EmailOutboxEntity>> GetUnprocessedEmailEntity();
}

internal class MongoDbGetEmailsFromOutboxService(IMongoCollection<EmailOutboxEntity> emailCollection)
  : IGetEmailsFromOutboxService
{
  public async Task<Result<EmailOutboxEntity>> GetUnprocessedEmailEntity()
  {
    FilterDefinition<EmailOutboxEntity> unprocessedFilter = Builders<EmailOutboxEntity>.Filter
      .Eq(entity => entity.DateTimeUtcProcessed, null);
    EmailOutboxEntity? unsentEmailEntity = await emailCollection.Find(unprocessedFilter).FirstOrDefaultAsync();

    if (unsentEmailEntity is null) return Result.NotFound();

    return unsentEmailEntity;
  }
}

internal class DefaultSendEmailsFromOutboxService(IGetEmailsFromOutboxService outboxService, ISendEmail emailSender,
  IMongoCollection<EmailOutboxEntity> emailCollection, ILogger<DefaultSendEmailsFromOutboxService> logger) : ISendEmailsFromOutboxService
{
  public async Task CheckForAndSendEmailsAsync()
  {
    try
    {
      Result<EmailOutboxEntity> result = await outboxService.GetUnprocessedEmailEntity();
      if (result.IsNotFound()) return;

      EmailOutboxEntity email = result.Value;
      await emailSender.SendEmailAsync(email.To, email.From, email.Subject, email.Body);

      FilterDefinition<EmailOutboxEntity> updateFilter = Builders<EmailOutboxEntity>
        .Filter.Eq(x => x.Id, email.Id);
      UpdateDefinition<EmailOutboxEntity> update = Builders<EmailOutboxEntity>
        .Update.Set(nameof(EmailOutboxEntity.DateTimeUtcProcessed), DateTime.UtcNow);
      UpdateResult updateResult = await emailCollection.UpdateOneAsync(updateFilter, update);
      logger.LogInformation("Processed {Result} email record(s)", updateResult.ModifiedCount);
    }
    finally
    {
      logger.LogInformation("Sleeping...");
    }
  }
}
