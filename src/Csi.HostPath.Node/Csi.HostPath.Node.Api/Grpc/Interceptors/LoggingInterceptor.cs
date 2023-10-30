using Grpc.Core;
using Grpc.Core.Interceptors;
using Serilog;
using Serilog.Context;

namespace Csi.HostPath.Node.Api.Grpc.Interceptors;

public class LoggingInterceptor : Interceptor
{
    private readonly ILogger<LoggingInterceptor> _logger;

    public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var response = await continuation(request, context);

        using (LogContext.PushProperty("RequestData", request))
        using (LogContext.PushProperty("ResponseData", response))
        {
            _logger.LogInformation("Request executed");    
        }
        
        return response;
    }
}