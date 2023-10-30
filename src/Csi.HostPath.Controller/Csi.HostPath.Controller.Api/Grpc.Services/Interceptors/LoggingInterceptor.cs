using Grpc.Core;
using Grpc.Core.Interceptors;
using Serilog.Context;

namespace Csi.HostPath.Controller.Api.Grpc.Services.Interceptors;

public class LoggingInterceptor : Interceptor
{
    private readonly ILogger<ExceptionInterceptor> _logger;

    public LoggingInterceptor(ILogger<ExceptionInterceptor> logger)
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