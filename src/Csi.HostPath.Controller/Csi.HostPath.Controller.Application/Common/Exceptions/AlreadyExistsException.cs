namespace Csi.HostPath.Controller.Application.Common.Exceptions;

public class AlreadyExistsException : ServiceLogicException
{
    public AlreadyExistsException(string message) : base(message) { }
}