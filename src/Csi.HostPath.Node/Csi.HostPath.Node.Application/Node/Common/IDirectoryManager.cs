namespace Csi.HostPath.Node.Application.Node.Common;

public interface IDirectoryManager
{
    void EnsureExists(string path);
}