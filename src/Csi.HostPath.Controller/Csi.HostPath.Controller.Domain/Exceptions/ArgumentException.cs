namespace Csi.HostPath.Controller.Domain.Exceptions;

public class ArgumentException : Exception
{
    public string Argument { get; }
    
    public ArgumentException(string argument, string message) : base(message)
    {
        Argument = argument;
    }
}