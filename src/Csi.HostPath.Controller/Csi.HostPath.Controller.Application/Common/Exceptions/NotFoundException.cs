namespace Csi.HostPath.Controller.Application.Common.Exceptions;

public class NotFoundException : ServiceLogicException
{
    public NotFoundException(string message) : base(message) { }
}