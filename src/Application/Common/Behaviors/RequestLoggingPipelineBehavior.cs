using MediatR;

using Microsoft.Extensions.Logging;

using Serilog.Context;


namespace MRA.AssetsManagement.Application.Common.Behaviors;

public class RequestLoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public RequestLoggingPipelineBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        try
        {
            TResponse result = await next();
            _logger.LogInformation("Completed request {RequestName}", requestName);
            return result;
        }
        catch (Exception ex)
        {
            using (LogContext.PushProperty("Error", ex, true))
            {
                _logger.LogError(
                    "Completed request {RequestName} with error ",
                    requestName
                );
            }
            throw;
        }
    }
}