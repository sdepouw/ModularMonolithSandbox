﻿using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Interfaces;

internal interface IApplicationUserRepository
{
  Task<ApplicationUser?> GetUserWithCartByEmailAsync(string email);
  Task<ApplicationUser?> GetUserWithAddressesByEmailAsync(string email);
  Task SaveChangesAsync();
}
