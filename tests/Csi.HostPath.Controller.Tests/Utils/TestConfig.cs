namespace Csi.HostPath.Controller.Tests.Utils;

public class TestConfig
{
    public static string ConnectionString => Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "http://localhost:6000";
}