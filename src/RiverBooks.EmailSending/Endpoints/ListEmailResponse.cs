namespace RiverBooks.EmailSending.Endpoints;

internal record ListEmailResponse(long Count, List<EmailOutboxEntity> Emails);
