namespace Csi.HostPath.Node.Application.Node.Common;

public interface IMounter
{
    void Mount(string source, string target, bool readOnly = false);
    void Unmount(string targetPath);
}