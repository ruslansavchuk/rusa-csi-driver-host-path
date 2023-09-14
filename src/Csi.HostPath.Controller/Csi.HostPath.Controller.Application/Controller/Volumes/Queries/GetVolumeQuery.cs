using Csi.HostPath.Controller.Domain.Entities;
using MediatR;

namespace Csi.HostPath.Controller.Application.Controller.Volumes.Queries;

public record GetVolumeQuery(string Id) : IRequest<Volume>;