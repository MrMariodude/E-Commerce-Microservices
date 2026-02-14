using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        //! 1) Log the exception first for logs purposes
        logger.LogError(
            exception,
            "Error message: {Message}. Time of occurrence {OccurredAtUtc}",
            exception.Message,
            DateTime.UtcNow
        );
        //! 2) detect the exact type of the exception and create the problem details
        (string Detail, string Title, int StatusCode) problem = exception switch
        {
            InternalServerException => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status500InternalServerError
            ),
            BadRequestException => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status400BadRequest
            ),
            NotFoundException => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status404NotFound
            ),
            ValidationException => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status400BadRequest
            ),
            _ => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status500InternalServerError
            ),
        };
        ProblemDetails problemDetails = new()
        {
            Title = problem.Title,
            Detail = problem.Detail,
            Status = problem.StatusCode,
            Instance = context.Request.Path,
        };
        problemDetails.Extensions.Add("traceID", context.TraceIdentifier);
        //! 3) In case of validation Exception we add the errors
        if (exception is ValidationException validationException)
        {
            //? This is just to putify the errors to the client
            var errors = validationException
                .Errors.GroupBy(error => error.PropertyName)
                .ToDictionary(
                    gruop => gruop.Key,
                    gruop => gruop.Select(error => error.ErrorMessage)
                )
                .ToList();
            problemDetails.Extensions.Add("ValidationErrors", errors);
        }

        context.Response.StatusCode = problem.StatusCode;
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}
