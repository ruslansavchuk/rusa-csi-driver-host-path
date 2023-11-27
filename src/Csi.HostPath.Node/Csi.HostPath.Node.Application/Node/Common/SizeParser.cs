using System.Text.RegularExpressions;

namespace Csi.HostPath.Node.Application.Node.Common;

public static class SizeParser
{
    public static bool TryParseSize(string input, out long size)
    {
        const long kiloBytes = 1024;
        const long megabytes = kiloBytes * 1024;
        const long gigaBytes = megabytes * 1024;

        var match = Regex.Match(input, @"^(\d+)([KkMmGg]i?)?$");

        if (!match.Success)
        {
            size = -1;
            return false;
        }

        var value = long.Parse(match.Groups[1].Value);
        var unit = match.Groups[2].Value.ToLower();

        size = unit switch
        {
            "k" or "ki" => value * kiloBytes,
            "m" or "mi" => value * megabytes,
            "g" or "gi" => value * gigaBytes,
            _ => value
        };

        return true;
    }
}