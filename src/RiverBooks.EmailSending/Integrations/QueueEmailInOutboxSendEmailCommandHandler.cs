using Ardalis.Result;
using MediatR;

namespace RiverBooks.EmailSending.Integrations;

internal class QueueEmailInOutboxSendEmailCommandHandler(IOutboxService outboxService) : IRequestHandler<SendEmailCommand, Result<Guid>>
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
