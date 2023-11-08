using Csi.HostPath.Controller.Tests.Utils;
using Csi.HostPath.Controller.Tests.Utils.DataGenerators;
using Csi.V1;
using Grpc.Net.Client;

namespace Csi.HostPath.Controller.Tests.Controller;

public abstract class ControllerTestsBase : IDisposable
{
    private readonly List<Volume> _volumesToCleanUp;
    private readonly V1.Controller.ControllerClient _client;

    protected ControllerTestsBase()
    {
        _volumesToCleanUp = new List<Volume>();
        var chanel = GrpcChannel.ForAddress(TestConfig.ConnectionString);
        _client = new V1.Controller.ControllerClient(chanel);
    }

    protected CreateVolumeResponse CreateVolume(CreateVolumeRequest request)
    {
        var response = _client.CreateVolume(request);
        _volumesToCleanUp.Add(response.Volume);
        
        return response;
    }

    protected DeleteVolumeResponse DeleteVolume(DeleteVolumeRequest request)
    {
        return _client.DeleteVolume(request);
    }

    protected GetCapacityResponse GetCapacity(GetCapacityRequest request)
    {
        return _client.GetCapacity(request);
    }

    protected ControllerGetCapabilitiesResponse GetCapabilities(ControllerGetCapabilitiesRequest request)
    {
        return _client.ControllerGetCapabilities(request);
    }

    public void Dispose()
    {
        foreach (var volume in _volumesToCleanUp)
        {
            _client.DeleteVolume(VolumeDataGenerator.GenerateDeleteVolumeRequest(volume.VolumeId));
        }
    }
}