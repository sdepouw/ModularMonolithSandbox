﻿using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.UserEndpoints;

internal class Create(UserManager<ApplicationUser> userManager) : Endpoint<CreateRequest>
{
  public override void Configure()
  {
    Post("/users");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateRequest request, CancellationToken cancellationToken)
  {
    ApplicationUser newUser = new()
    {
      Email = request.Email,
      UserName = request.Email
    };
    await userManager.CreateAsync(newUser, request.Password);
    await SendOkAsync(cancellationToken);
  }
}
