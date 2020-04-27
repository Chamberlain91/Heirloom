# CpuInfo

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Platforms.Desktop][0]  
> **Dependencies**: [Heirloom.Core][1], [Heirloom.OpenGLES][2], [Heirloom.MiniAudio][3]  
> **Namespace**: [Heirloom.Desktop.Hardware][0]  

Contains information related to the CPU.

```cs
public struct CpuInfo : IEquatable<CpuInfo>
```

--------------------------------------------------------------------------------

**Inherits**: IEquatable\<CpuInfo>

**Properties**: [Vendor][4], [Name][5], [ThreadCount][6], [ClockSpeed][7]

--------------------------------------------------------------------------------

## Constructors

### CpuInfo(string, int, int)

```cs
CpuInfo(string name, int clockSpeed, int threadCount)
```

### CpuInfo(CpuVendor, string, int, int)

```cs
CpuInfo(CpuVendor vendor, string name, int clockSpeed, int threadCount)
```

## Properties

| Name             | Summary                                                  |
|------------------|----------------------------------------------------------|
| [Vendor][4]      | Gets the CPU Vendor.                                     |
| [Name][5]        | Gets the name of the CPU.                                |
| [ThreadCount][6] | Gets how many threads (logical cores) this CPU supports. |
| [ClockSpeed][7]  | Gets the clockspeed of this CPU in MHz.                  |

[0]: ..\Heirloom.Platforms.Desktop.md
[1]: ..\Heirloom.Core.md
[2]: ..\Heirloom.OpenGLES.md
[3]: ..\Heirloom.MiniAudio.md
[4]: Heirloom.Desktop.Hardware.CpuInfo.Vendor.md
[5]: Heirloom.Desktop.Hardware.CpuInfo.Name.md
[6]: Heirloom.Desktop.Hardware.CpuInfo.ThreadCount.md
[7]: Heirloom.Desktop.Hardware.CpuInfo.ClockSpeed.md
