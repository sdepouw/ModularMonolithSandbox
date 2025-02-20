namespace RiverBooks.OrderProcessing.Contracts;

/// <summary>
/// Basic details of the order
/// TODO: Include address info for geographic specific reports to use
/// </summary>
public record OrderDetailsDTO(DateTimeOffset DateCreated, Guid OrderId, Guid UserId, List<OrderItemDetails> OrderItems);
