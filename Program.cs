using Spectre.Console;
using Myco;

[assembly: System.Runtime.Versioning.SupportedOSPlatformAttribute("windows")]

var cpu = Info.GetCpuInfo();
var gpu = Info.GetGpuInfo();
var os = Info.GetOsInfo();
var memories = Info.GetMemoryInfo();

var totalMemoryCapacity = memories.Sum(x => (decimal)x.Capacity);
var memoryInGb = totalMemoryCapacity / (1024 * 1024 * 1024);
var osString = $"{os.Caption} {os.Version} {os.OsArchitecture}";
var cpuClockSpeed = (decimal)cpu.MaxClockSpeed / 1000;

var cpuRender =
        new Grid()
            .AddColumn()
            .AddColumn()
            .AddRow("[white]OS[/]", $"[white]{osString}[/]")
            .AddRow("[blue]CPU[/]", $"[blue]{cpu.Name}[/]")
            .AddRow("[blue]Cores[/]", $"[blue]{cpu.NumberOfCores}[/]")
            .AddRow("[blue]Logical Processors[/]", $"[blue]{cpu.NumberOfLogicalProcessors}[/]")
            .AddRow("[blue]Base Speed[/]", $"[blue]{cpuClockSpeed} GHz[/]")
            .AddRow("[green]GPU[/]", $"[green]{gpu.Name}[/]")
            .AddRow("[grey]Memory[/]", $"[grey]{memories.First().Manufacturer}[/]")
            .AddRow("[grey]Capacity[/]", $"[grey]{memoryInGb} GB[/]")
            .AddRow("[grey]Speed[/]", $"[grey]{memories.First().Speed} MHz[/]")
    ;

AnsiConsole.Write(cpuRender);