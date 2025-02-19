using Ardalis.Result;

namespace RiverBooks.OrderProcessing.MaterializedViews;

internal interface IOrderAddressCache
{
  Task<Result<OrderAddress>> GetByIdAsync(Guid addressId);
}

/// <summary>
/// This is the materialized view's data model
/// </summary>
internal record OrderAddress(Guid Id, Address Address);
