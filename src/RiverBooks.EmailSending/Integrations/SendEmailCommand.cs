using Ardalis.Result;
using MediatR;

namespace RiverBooks.EmailSending.Integrations;

public record SendEmailCommand(string To, string From, string Subject, string Body) : IRequest<Result<Guid>>;
