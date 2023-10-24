namespace Csi.HostPath.Node.Application.Node.Common;

public interface IMounter
{
    void Mount(string source, string target, string[] options);
    void Unmount(string targetPath);
}