using FastEndpoints;
using FluentValidation;

namespace RiverBooks.Books.BookEndpoints;

internal class UpdatePriceRequestValidator : Validator<UpdatePriceRequest>
{
  public UpdatePriceRequestValidator()
  {
    RuleFor(x => x.Id)
      .NotNull()
      .NotEqual(Guid.Empty)
      .WithMessage("A book ID is required.");

    RuleFor(x => x.NewPrice)
      .GreaterThanOrEqualTo(0m)
      .WithMessage("Book prices may not be negative.");
  }
}
