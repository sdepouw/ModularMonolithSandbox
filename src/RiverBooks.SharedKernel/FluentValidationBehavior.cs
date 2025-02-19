using System.Reflection;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace RiverBooks.SharedKernel;

public class FluentValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) :
  IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  public async Task<TResponse> Handle(TRequest request,
    RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    if (!validators.Any()) return await next();

    ValidationContext<TRequest> context = new(request);

    ValidationResult[] validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
    List<ValidationError> resultErrors = validationResults.SelectMany(r => r.AsErrors()).ToList();
    List<ValidationFailure> failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

    if (failures.Count == 0) return await next();
    
    if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
    {
      Type resultType = typeof(TResponse).GetGenericArguments()[0];
      MethodInfo? invalidMethod = typeof(Result<>)
        .MakeGenericType(resultType)
        .GetMethod(nameof(Result<int>.Invalid), [typeof(List<ValidationError>)]);

      if (invalidMethod is not null)
      {
        return (TResponse)invalidMethod.Invoke(null, [resultErrors])!;
      }
    }
    else if (typeof(TResponse) == typeof(Result))
    {
      return (TResponse)(object)Result.Invalid(resultErrors);
    }
    else
    {
      throw new ValidationException(failures);
    }
    return await next();
  }
}
