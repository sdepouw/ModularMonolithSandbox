﻿using Ardalis.Result;

namespace RiverBooks.EmailSending.Integrations;

internal class SendEmailCommandHandler(ISendEmail emailSender) //: IRequestHandler<SendEmailCommand, Result<Guid>>
{
  public async Task<Result<Guid>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
  {
    await emailSender.SendEmailAsync(request.To, request.From, request.Subject, request.Body);

    return Guid.Empty;
  }
}
