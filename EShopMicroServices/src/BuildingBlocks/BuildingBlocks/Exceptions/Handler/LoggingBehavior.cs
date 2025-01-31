using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Exceptions.Handler;

public class LoggingBehavior<TRequest, TResponse>
    (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request={Request} - Response{Response} - RequestData={RequestData}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();

        var timetaken = timer.Elapsed;
        if (timetaken.Seconds > 10)
        {
            logger.LogWarning("[Performance] The request {Request} took {TimeTaken} Seconds",
                typeof(TRequest).Name, timetaken.Seconds);

        }
            logger.LogInformation("[END] Handled {Request} with {Response}", typeof(TRequest).Name, timetaken.Seconds);
            return response;
    }
}
