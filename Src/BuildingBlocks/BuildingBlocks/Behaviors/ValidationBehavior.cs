using BuildingBlocks.CQRS;
using Concordia;
using FluentValidation;

namespace BuildingBlocks.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse> //! -> Why would we only need to validate the command requests only?
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context, cancellationToken))
        );
        var failures = validationResults
            .Where(result => result.Errors.Count != 0)
            .SelectMany(result => result.Errors)
            .ToList();
        if (failures.Count != 0)
            throw new ValidationException(failures);
        return await next(cancellationToken);
    }
}
