using Grpc.Core;
using Grpc.Core.Interceptors;
using Serilog;

namespace Csi.HostPath.Node.Api.Grpc.Interceptors;

public class LoggingInterceptor : Interceptor
{
    private readonly ILogger<ExceptionInterceptor> _logger;
    private readonly IDiagnosticContext _diagnosticContext;

    public LoggingInterceptor(
        ILogger<ExceptionInterceptor> logger, 
        IDiagnosticContext diagnosticContext)
    {
        _logger = logger;
        _diagnosticContext = diagnosticContext;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        _diagnosticContext.Set("RequestData", request);
        try
        {
            var response = await continuation(request, context);
            _diagnosticContext.Set("ResponseData", response);
            return response;
        }
        finally
        {
            _logger.LogInformation("Request executed");
        }
    }
}