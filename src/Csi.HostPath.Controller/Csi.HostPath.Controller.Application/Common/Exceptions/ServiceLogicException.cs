namespace Csi.HostPath.Controller.Application.Common.Exceptions;

public class ServiceLogicException : Exception
{
    public ServiceLogicException(string message): base(message) {}
}