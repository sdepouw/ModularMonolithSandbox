using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RiverBooks.EmailSending.EmailBackgroundService;

internal class EmailSendingBackgroundService(ILogger<EmailSendingBackgroundService> logger,
  ISendEmailsFromOutboxService sendEmailsFromOutboxService) : BackgroundService
{
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    const int delayMilliseconds = 10_000; // 10 seconds

    logger.LogInformation("{ServiceName} starting...", nameof(EmailSendingBackgroundService));

    while (!stoppingToken.IsCancellationRequested)
    {
      try
      {
        await sendEmailsFromOutboxService.CheckForAndSendEmailsAsync();
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Error processing outbox");
      }
      finally
      {
        await Task.Delay(delayMilliseconds, stoppingToken);
      }
    }

    logger.LogInformation("{ServiceName} stopping!", nameof(EmailSendingBackgroundService));
  }
}
