using Csi.HostPath.Node.Application.Node.Common;

namespace Csi.HostPath.Node.Infrastructure.Mounter.Linux;

public class LinuxMounter : IMounter
{
    private const string MountUtil = "mount";
    private const string UnmountUtil = "umount";

    public void Mount(string source, string target, bool readOnly = false)
    {
        var options = new List<string> {"--bind"};

        if (readOnly)
        {
            options.AddRange(new []{"-o", "ro"});
        }

        var commandOptions = BuildMoundOptions(source, target, options.ToArray());
        var mountCommand = new Command(MountUtil, commandOptions);
        var (_, stdError, status) = mountCommand.Exec();
        if (status > 0)
        {
            throw new Exception(stdError);
        }
    }

    private string[] BuildMoundOptions(string source, string target, string[] options)
    {
        var result = new string[2 + options?.Length ?? 0];
        result[0] = source;
        result[1] = target;

        if (options == null) return result;
        
        for (var i =0; i< options.Length; i++)
        {
            result[2 + i] = options[i];
        }

        return result;
    }

    public void Unmount(string targetPath)
    {
        var unmountCommand = new Command(UnmountUtil, new[] {targetPath});
        var (_, stdError, status) = unmountCommand.Exec();
        if (status > 0)
        {
            throw new Exception(stdError);
        }
    }
}