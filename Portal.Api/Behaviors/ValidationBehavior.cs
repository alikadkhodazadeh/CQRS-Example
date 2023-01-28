using Microsoft.Extensions.Caching.Memory;
using Portal.Api.CQRS.Queries;
using System.Reflection;

namespace Portal.Api.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            ValidationContext<TRequest> validationContext = new(request);
            var validation = _validators.Select(x => x.ValidateAsync(validationContext, cancellationToken));
            var result = await Task.WhenAll(validation);
            var errors = result.SelectMany(x => x.Errors).Where(x=>x is not null).ToHashSet();
            if(errors.Count > 0)
                throw new ValidationException(errors);
        }

        return await next();
    }
}
