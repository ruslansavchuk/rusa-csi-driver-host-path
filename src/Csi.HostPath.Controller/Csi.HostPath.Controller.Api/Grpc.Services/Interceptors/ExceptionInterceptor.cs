using Csi.HostPath.Controller.Application.Common.Exceptions;
using FluentValidation;
using Grpc.Core;
using Grpc.Core.Interceptors;

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
        catch (AlreadyExistsException ex)
        {
            throw new RpcException(new Status(StatusCode.AlreadyExists, ex.Message));
        }
        catch (NotFoundException ex)
        {
            throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
        }
        catch (ServiceLogicException ex)
        {
            throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
        }
        catch (ValidationException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }
    }
}