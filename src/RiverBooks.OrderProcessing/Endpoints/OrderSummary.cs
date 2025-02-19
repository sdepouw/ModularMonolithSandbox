namespace RiverBooks.OrderProcessing.Endpoints;

internal record OrderSummary(Guid OrderId, Guid UserId, DateTimeOffset DateCreated, DateTimeOffset? DateShipped, decimal Total);
