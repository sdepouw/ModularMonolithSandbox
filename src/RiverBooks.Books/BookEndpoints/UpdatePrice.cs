﻿using FastEndpoints;
using RiverBooks.Books.Interfaces;

namespace RiverBooks.Books.BookEndpoints;

internal class UpdatePrice(IBookService bookService) : Endpoint<UpdatePriceRequest, BookDTO>
{
  public override void Configure()
  {
    // Normally a PUT, but we're doing more RPC-style.
    // We're pretending there's a full price history. We may have a GET history later.
    Post("/books/{Id}/pricehistory");
    AllowAnonymous();
  }

  public override async Task HandleAsync(UpdatePriceRequest request, CancellationToken cancellationToken)
  {
    // TODO: Handle Not Found gracefully
    await bookService.UpdateBookPriceAsync(request.Id, request.NewPrice);

    BookDTO? updatedBook = await bookService.GetBookByIdAsync(request.Id);

    await SendAsync(updatedBook!, cancellation: cancellationToken);
  }
}
