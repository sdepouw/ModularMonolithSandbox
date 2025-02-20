using Ardalis.Result;
using MediatR;
using MongoDB.Driver;

namespace RiverBooks.EmailSending.Integrations;

internal interface IQueueEmailsInOutboxService
{
  Task QueueEmailForSending(EmailOutboxEntity entity);
}

internal class MongoDbQueueEmailOutboxService(IMongoCollection<EmailOutboxEntity> emailCollection) : IQueueEmailsInOutboxService
{
  public Task QueueEmailForSending(EmailOutboxEntity entity) => emailCollection.InsertOneAsync(entity);
}

internal class QueueEmailInOutboxSendEmailCommandHandler(IQueueEmailsInOutboxService outboxService)
  : IRequestHandler<SendEmailCommand, Result<Guid>>
{
  public async Task<Result<Guid>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
  {
    EmailOutboxEntity outboxEntity = new()
    {
      To = request.To,
      From = request.From,
      Subject = request.Subject,
      Body = request.Body
    };
    await outboxService.QueueEmailForSending(outboxEntity);

    return outboxEntity.Id;
  }
}
