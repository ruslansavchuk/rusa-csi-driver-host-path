using Csi.HostPath.Controller.Tests.Utils;
using Grpc.Net.Client;

namespace Csi.HostPath.Controller.Tests.Controller;

public abstract class ControllerTestsBase
{
    protected readonly V1.Controller.ControllerClient Client;

    protected ControllerTestsBase()
    {
        var chanel = GrpcChannel.ForAddress(TestConfig.ConnectionString);
        Client = new V1.Controller.ControllerClient(chanel);
    }
}