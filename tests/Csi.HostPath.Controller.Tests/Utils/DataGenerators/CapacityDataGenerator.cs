namespace Csi.HostPath.Controller.Tests.Utils.DataGenerators;

public static class CapacityDataGenerator
{
    public static long Kilobytes(long kb)
    {
        return kb * 1024;
    }

    public static long Megabytes(long mb)
    {
        return Kilobytes(mb * 1024);
    }

    public static long Gigabytes(long gb)
    {
        return Megabytes(gb * 1024);
    }
}