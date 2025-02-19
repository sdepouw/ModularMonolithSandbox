using RiverBooks.OrderProcessing.Domain;

namespace RiverBooks.OrderProcessing.Infrastructure;

/// <summary>
/// This is the materialized view's data model
/// </summary>
internal record OrderAddress(Guid Id, Address Address);
