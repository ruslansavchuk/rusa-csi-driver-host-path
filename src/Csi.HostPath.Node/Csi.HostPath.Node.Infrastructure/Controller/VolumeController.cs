using Csi.HostPath.Node.Application.Common.Controller;
using Csi.V1;
using Volume = Csi.HostPath.Node.Application.Common.Controller.Dtos.Volume;

namespace Csi.HostPath.Node.Infrastructure.Controller;

public class VolumeController : IVolumeController
{
    private readonly V1.Controller.ControllerClient _client;

    public VolumeController(V1.Controller.ControllerClient client)
    {
        _client = client;
    }

    public async Task<List<Volume>> ListVolumes(CancellationToken cancellationToken)
    {
        var volumes = new List<Volume>();

        var startingToken = string.Empty;
        
        do
        {
            var listVolumesRequest = new ListVolumesRequest
            {
                StartingToken = startingToken
            };
            
            var response = await _client.ListVolumesAsync(listVolumesRequest, cancellationToken: cancellationToken);
            volumes.AddRange(response.Entries.Select(e => new Volume(
                e.Volume.VolumeId,
                e.Volume.VolumeContext["volume-name"],
                e.Volume.VolumeContext.ToDictionary(
                    i => i.Key,
                    i => i.Value))));

            startingToken = response.NextToken;
        } while (!string.IsNullOrWhiteSpace(startingToken));

        return volumes;
    }

    public async Task<Volume> Create(string volumeName, long capacity, CancellationToken cancellationToken)
    {
        var createVolumeRequest = new CreateVolumeRequest
        {
            Name = volumeName,
            CapacityRange = new CapacityRange
            {
                LimitBytes = capacity,
                RequiredBytes = capacity
            }
        };
        
        createVolumeRequest.VolumeCapabilities.Add(new VolumeCapability
        {
            Mount = new VolumeCapability.Types.MountVolume(),
            AccessMode = new VolumeCapability.Types.AccessMode
            {
                Mode = VolumeCapability.Types.AccessMode.Types.Mode.SingleNodeSingleWriter
            }
        });

        var response = await _client.CreateVolumeAsync(createVolumeRequest, cancellationToken: cancellationToken);

        return new Volume(response.Volume.VolumeId,
            volumeName,
            response.Volume.VolumeContext.ToDictionary(
                i => i.Key,
                i => i.Value));
    }

    public async Task Delete(string volumeId, CancellationToken cancellationToken)
    {
        var deleteVolumeRequest = new DeleteVolumeRequest
        {
            VolumeId = volumeId
        };

        await _client.DeleteVolumeAsync(deleteVolumeRequest, cancellationToken: cancellationToken);
    }

    public async Task Publish(string volumeId, string nodeId, CancellationToken cancellationToken)
    {
        var publishVolumeRequest = new ControllerPublishVolumeRequest
        {
            VolumeId = volumeId,
            NodeId = nodeId,
            Readonly = false,
            VolumeCapability = new VolumeCapability
            {
                Mount = new VolumeCapability.Types.MountVolume(),
                AccessMode = new VolumeCapability.Types.AccessMode
                {
                    Mode = VolumeCapability.Types.AccessMode.Types.Mode.SingleNodeSingleWriter
                }
            }
        };

        await _client.ControllerPublishVolumeAsync(publishVolumeRequest, cancellationToken: cancellationToken);
    }

    public async Task Unpublish(string volumeId, string nodeId, CancellationToken cancellationToken)
    {
        var unpublishVolumeRequest = new ControllerUnpublishVolumeRequest
        {
            VolumeId = volumeId,
            NodeId = nodeId
        };

        await _client.ControllerUnpublishVolumeAsync(unpublishVolumeRequest, cancellationToken: cancellationToken);
    }
}