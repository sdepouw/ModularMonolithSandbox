namespace RiverBooks.Users.UserEndpoints;

internal record UserAddressDTO(Guid Id, string Street1, string Street2, string City, string State, string PostalCode, string Country);
