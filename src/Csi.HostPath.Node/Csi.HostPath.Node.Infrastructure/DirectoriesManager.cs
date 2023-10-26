using Csi.HostPath.Node.Application.Node.Common;

namespace Csi.HostPath.Node.Infrastructure;

public class DirectoriesManager : IDirectoryManager
{
    public void EnsureExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}