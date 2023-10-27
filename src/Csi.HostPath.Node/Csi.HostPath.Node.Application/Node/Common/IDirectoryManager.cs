namespace Csi.HostPath.Node.Application.Node.Common;

public interface IDirectoryManager
{
    void EnsureExists(string path);
    long GetUsedBytes(string path);
    HashSet<string> GetSubFolders(string path);
}