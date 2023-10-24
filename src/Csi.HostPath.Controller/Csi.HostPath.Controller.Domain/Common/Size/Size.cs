namespace Csi.HostPath.Controller.Domain.Common.Size;

public record Size(long Bytes)
{
    public static implicit operator long(Size  size) => size.Bytes;
    public static implicit operator Size(long bytes) => new(bytes);
}