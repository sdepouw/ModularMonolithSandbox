namespace RiverBooks.Users.UseCases.Cart.Checkout;

internal record CheckoutRequest(Guid ShippingAddressId, Guid BillingAddressId);
