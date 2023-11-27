namespace Csi.HostPath.Node.Application.Node.Publish;

public static class EphemeralVolumeHelper
{
    public static bool IsEphemeral(Dictionary<string, string> volumeContext)
        => volumeContext.TryGetValue("csi.storage.k8s.io/ephemeral", out var value)
           && bool.TryParse(value, out var ephemeral)
           && ephemeral;

    public static string GetVolumeStageDir(string dataDir, string volumeId) =>
        Path.Combine(dataDir, "ephemeral", volumeId);
}