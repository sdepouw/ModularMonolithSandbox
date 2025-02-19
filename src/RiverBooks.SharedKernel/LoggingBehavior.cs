using System.Diagnostics;
using System.Reflection;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RiverBooks.SharedKernel;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
  : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    Guard.Against.Null(request);
    if (logger.IsEnabled(LogLevel.Information))
    {
      logger.LogInformation("Handling {RequestName}", typeof(TRequest).Name);

      // Reflection! Could be a performance concern
      Type myType = request.GetType();
      IList<PropertyInfo> properties = new List<PropertyInfo>(myType.GetProperties());
      foreach (PropertyInfo property in properties)
      {
        object? propertyValue = property.GetValue(request, null);
        logger.LogInformation("Property {PropertyName}: {@Value}", property.Name, propertyValue);
      }
    }

    Stopwatch sw = Stopwatch.StartNew();
    TResponse response = await next();
    logger.LogInformation("Handled {RequestName} with {Response} in {Milliseconds} ms",
      typeof(TRequest).Name, response, sw.ElapsedMilliseconds);
    sw.Stop();
    return response;
  }
}
