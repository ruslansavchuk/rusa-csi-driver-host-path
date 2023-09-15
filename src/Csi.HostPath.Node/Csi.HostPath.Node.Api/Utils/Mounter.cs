namespace Csi.HostPath.Node.Api.Utils;

public class Mounter
{
    private const string MountUtil = "mount";
    private const string UnmountUtil = "unmount";

    public void Mount(string source, string target, string[] options)
    {
        var commandOptions = BuildMoundOptions(source, target, options);
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