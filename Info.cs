using System.Management;
using System.Runtime.Versioning;

namespace Myco;

internal record CpuInfo(
    string Name,
    uint NumberOfCores,
    uint NumberOfLogicalProcessors,
    uint MaxClockSpeed,
    uint CurrentClockSpeed,
    uint L2CacheSize,
    uint L3CacheSize,
    string SocketDesignation
);

internal record GpuInfo(
    string Name
);

internal record OsInfo(string Caption, string Version, string OsArchitecture);

internal record MemoryInfo(ulong Capacity, string Manufacturer, string PartNumber, uint Speed);

internal record DriveInfo(string Model, ulong Size);
[SupportedOSPlatform("windows")]

internal static class Info
{
    internal static CpuInfo GetCpuInfo()
    {
        using var searcher = new ManagementObjectSearcher("select * from Win32_Processor");
        using var collection = searcher.Get();
        var obj = collection.Cast<ManagementObject>().First();
        var cpu = new CpuInfo(
            (string)obj["Name"],
            (uint)obj["NumberOfCores"],
            (uint)obj["NumberOfLogicalProcessors"],
            (uint)obj["MaxClockSpeed"],
            (uint)obj["CurrentClockSpeed"],
            (uint)obj["L2CacheSize"],
            (uint)obj["L3CacheSize"],
            (string)obj["SocketDesignation"]
        );
        return cpu;
    }

    internal static GpuInfo GetGpuInfo()
    {
        using var searcher = new ManagementObjectSearcher("select * from Win32_VideoController");
        using var collection = searcher.Get();
        var obj = collection.Cast<ManagementObject>().First();
        var gpu = new GpuInfo(
            (string)obj["Name"]
        );
        return gpu;
    }

    internal static OsInfo GetOsInfo()
    {
        using var searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
        using var collection = searcher.Get();
        var obj = collection.Cast<ManagementObject>().First();
        var os = new OsInfo(
            (string)obj["Caption"],
            (string)obj["Version"],
            (string)obj["OSArchitecture"]
        );
        return os;
    }

    internal static List<MemoryInfo> GetMemoryInfo()
    {
        using var searcher = new ManagementObjectSearcher("select * from Win32_PhysicalMemory");
        using var collection = searcher.Get();
        var memories = new List<MemoryInfo>();
        foreach (var obj in collection.Cast<ManagementObject>())
        {
            var memory = new MemoryInfo(
                (ulong)(obj["Capacity"]),
                (string)obj["Manufacturer"],
                (string)obj["PartNumber"],
                (uint)obj["Speed"]
            );
            memories.Add(memory);
        }

        return memories;
    }

    internal static List<DriveInfo> GetDriveInfo()
    {
        using var searcher = new ManagementObjectSearcher("select * from Win32_DiskDrive");
        using var collection = searcher.Get();
        var drives = new List<DriveInfo>();
        foreach (var obj in collection.Cast<ManagementObject>())
        {
            var drive = new DriveInfo(
                (string)obj["Model"],
                (ulong)obj["Size"]
            );
            drives.Add(drive);
        }

        return drives;
    }
}