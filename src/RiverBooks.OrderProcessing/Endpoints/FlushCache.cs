using FastEndpoints;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace RiverBooks.OrderProcessing.Endpoints;

public class FlushCache(ILogger<FlushCache> logger) : EndpointWithoutRequest
{
  // TODO: Use DI
  // TODO: Get server from config
  private readonly IDatabase _redisDatabase = ConnectionMultiplexer.Connect("localhost").GetDatabase();

  public override void Configure()
  {
    Post("/flushcache");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    await _redisDatabase.ExecuteAsync("FLUSHDB");
    logger.LogWarning("FLUSHED CACHE FOR {DatabaseType}", "REDIS");
  }
}
