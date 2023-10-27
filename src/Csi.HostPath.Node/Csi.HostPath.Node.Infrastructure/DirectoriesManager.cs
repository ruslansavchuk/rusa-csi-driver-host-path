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

    public long GetUsedBytes(string path)
    {
        return Directory.GetFiles(path, "*", SearchOption.AllDirectories)
            .Select(filePath => new FileInfo(filePath))
            .Select(fileInfo => fileInfo.Length)
            .Sum();
    }

    public HashSet<string> GetSubFolders(string path)
    {
        return Directory.GetDirectories(path).ToHashSet();
    }
}