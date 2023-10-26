using Csi.HostPath.Node.Application.Node.Common;

namespace Csi.HostPath.Node.Infrastructure.Mounter.Windows;

public class WindowsMounter : IMounter
{
    public void Mount(string source, string target, bool readOnly = false)
    {
        // do nothing, it is just a mock
    }

    public void Unmount(string targetPath)
    {
        // do nothing, it is just a mock
    }
}