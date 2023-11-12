using Csi.HostPath.Controller.Tests.Utils;
using Csi.HostPath.Controller.Tests.Utils.DataGenerators;
using Csi.V1;
using Grpc.Net.Client;

namespace Csi.HostPath.Controller.Tests.Controller;

public abstract class ControllerTestsBase : IDisposable
{
    private readonly HashSet<string> _volumeIdsToDelete;
    private readonly Dictionary<string, HashSet<string>> _volumesToUnpublish;
    
    private readonly V1.Controller.ControllerClient _client;

    protected ControllerTestsBase()
    {
        _volumeIdsToDelete = new HashSet<string>();
        _volumesToUnpublish = new Dictionary<string, HashSet<string>>();
        var chanel = GrpcChannel.ForAddress(TestConfig.ConnectionString);
        _client = new V1.Controller.ControllerClient(chanel);
    }

    protected Func<CreateVolumeResponse> CreateVolume(CreateVolumeRequest request)
    {
        return () =>
        {
            var response = _client.CreateVolume(request);
            _volumeIdsToDelete.Add(response.Volume.VolumeId);
        
            return response;
        };
    }

    protected Func<DeleteVolumeResponse> DeleteVolume(DeleteVolumeRequest request)
    {
        return () =>
        {
            var response = _client.DeleteVolume(request);
            _volumeIdsToDelete.Remove(request.VolumeId);
            return response;
        }; 
    }

    protected Func<GetCapacityResponse> GetCapacity(GetCapacityRequest request)
    {
        return () => _client.GetCapacity(request);
    }

    protected Func<ControllerGetCapabilitiesResponse> GetCapabilities(ControllerGetCapabilitiesRequest request)
    {
        return () => _client.ControllerGetCapabilities(request);
    }

    protected Func<ControllerPublishVolumeResponse> PublishVolume(ControllerPublishVolumeRequest request)
    {
        return () =>
        {
            var response = _client.ControllerPublishVolume(request);
            
            if (_volumesToUnpublish.TryGetValue(request.VolumeId, out var publishedOn))
            {
                publishedOn.Add(request.NodeId);
            }
            else
            {
                _volumesToUnpublish.Add(request.VolumeId, new HashSet<string> {request.NodeId});
            }

            return response;
        };
    }

    protected Func<ControllerUnpublishVolumeResponse> UnpublishVolume(ControllerUnpublishVolumeRequest request)
    {
        return () =>
        {
            var response =  _client.ControllerUnpublishVolume(request);
            _volumesToUnpublish[request.VolumeId].Remove(request.NodeId);
            return response;
        };
    }

    public void Dispose()
    {
        // unpublish all published volumes
        foreach (var kvp in _volumesToUnpublish)
        {
            foreach (var nodeId in kvp.Value)
            {
                var command = VolumePublishDataGenerator.GenerateUnpublishVolumeCommand(kvp.Key, nodeId);
                _client.ControllerUnpublishVolume(command);
            }
        }
        
        // remove all created volumes
        foreach (var volumeId in _volumeIdsToDelete)
        {
            _client.DeleteVolume(VolumeDataGenerator.GenerateDeleteVolumeRequest(volumeId));
        }
    }
}