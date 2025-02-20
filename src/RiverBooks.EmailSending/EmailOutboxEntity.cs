namespace RiverBooks.EmailSending;

internal class EmailOutboxEntity
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public string To { get; set; } = "";
  public string From { get; set; } = "";
  public string Subject { get; set; } = "";
  public string Body { get; set; } = "";
  public DateTime? DateTimeUtcProcessed { get; set; }
}
