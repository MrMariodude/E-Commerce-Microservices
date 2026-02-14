using System.Diagnostics;
using System.Text.Json;
using Concordia;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation(
            $"⏳ [START] Handle request={typeof(TRequest).Name} - Response={typeof(TResponse).Name} - RequestData={JsonSerializer.Serialize(request)}"
        );

        var timer = new Stopwatch();
        timer.Start();

        var response = await next(cancellationToken);

        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
            logger.LogWarning(
                $"🚨 [PERFORMANCE] The request {typeof(TRequest).Name} took {timeTaken.Seconds} seconds."
            );

        logger.LogInformation(
            $"✅ [END] Handled {typeof(TRequest).Name} with {typeof(TResponse).Name}"
        );
        return response;
    }
}
