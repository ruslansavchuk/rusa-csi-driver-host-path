using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Csi.HostPath.Node.Api.Grpc.Interceptors;

public class ExceptionInterceptor  : Interceptor
{
    private readonly ILogger<ExceptionInterceptor> _logger;
 
    public ExceptionInterceptor(ILogger<ExceptionInterceptor> logger)
    {
        _logger = logger;
    }
     
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during handling of the request");
            throw;
        }
    }
}