using System.Diagnostics;

namespace Csi.HostPath.Node.Infrastructure.Mounter;

public class Command
{
    private readonly ProcessStartInfo _startInfo;

    public Command(string cmd, string[] args)
    {
        _startInfo = new ProcessStartInfo
        {
            FileName = cmd,
            Arguments = string.Join(" ", args),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
    }

    public (string, string, int) Exec()
    {
        var process = new Process
        {
            StartInfo = _startInfo
        };

        process.Start();

        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();
        
        process.WaitForExit();

        return (output, error, process.ExitCode);
    }
}