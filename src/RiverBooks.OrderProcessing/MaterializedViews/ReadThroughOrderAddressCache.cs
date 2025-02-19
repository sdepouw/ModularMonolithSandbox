using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.Users.Contracts;

namespace RiverBooks.OrderProcessing.MaterializedViews;

internal class ReadThroughOrderAddressCache(RedisOrderAddressCache redisCache, IMediator mediator,
  ILogger<ReadThroughOrderAddressCache> logger) : IOrderAddressCache
{
  public async Task<Result<OrderAddress>> GetByIdAsync(Guid addressId)
  {
    Result<OrderAddress> result = await redisCache.GetByIdAsync(addressId);
    if (result.IsSuccess) return result;
    if (result.IsNotFound())
    {
      // Fetch data form source
      logger.LogInformation("Address {Id} not found; fetching from source", addressId);
      UserAddressDetailsByIdQuery query = new UserAddressDetailsByIdQuery(addressId);
      Result<UserAddressDetails> queryResult = await mediator.Send(query);

      if (queryResult.IsSuccess)
      {
        UserAddressDetails dto = queryResult.Value;
        Address address = new Address(dto.Street1, dto.Street2, dto.City, dto.State, dto.PostalCode, dto.Country);
        OrderAddress orderAddress = new(dto.AddressId, address);
        await StoreAsync(orderAddress);
        return orderAddress;
      }
    }
    return Result.NotFound();
  }

  public Task<Result> StoreAsync(OrderAddress orderAddress) => redisCache.StoreAsync(orderAddress);
}
