using System.Text.Json;
using Ardalis.Result;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace RiverBooks.OrderProcessing.MaterializedViews;

internal class RedisOrderAddressCache(ILogger<RedisOrderAddressCache> logger) : IOrderAddressCache
{
  // TODO: Get server form config.
  private readonly IDatabase _redisDatabase = ConnectionMultiplexer.Connect("localhost").GetDatabase();

  public async Task<Result<OrderAddress>> GetByIdAsync(Guid addressId)
  {
    string? fetchedJson = await _redisDatabase.StringGetAsync(addressId.ToString());
    if (fetchedJson is null)
    {
      logger.LogWarning("Address {Id} not found in {DatabaseType}", addressId, "REDIS");
      return Result.NotFound();
    }

    OrderAddress? address = JsonSerializer.Deserialize<OrderAddress>(fetchedJson);
    if (address is null) return Result.NotFound();

    logger.LogInformation("Address {Id} returned from {DatabaseType}", addressId, "REDIS");
    return Result.Success(address);
  }
}
