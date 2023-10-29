using Csi.HostPath.Controller.Application.Common.Exceptions;
using FluentValidation;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Serilog.Context;

namespace Csi.HostPath.Controller.Api.Grpc.Services.Interceptors;

public class ExceptionInterceptor : Interceptor
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
            using (LogContext.PushProperty("RequestData", request))
            {
                _logger.LogError(ex, "Error occured during handling of the request");   
            }
            
            StatusCode? statusCode = ex switch
            {
                AlreadyExistsException => StatusCode.AlreadyExists,
                NotFoundException => StatusCode.NotFound,
                ServiceLogicException=> StatusCode.Unknown,
                ValidationException => StatusCode.InvalidArgument,
                _ => null
            };

            if (statusCode is null)
            {
                throw;
            }

            throw new RpcException(new Status(statusCode.Value, ex.Message));
        }
    }
}