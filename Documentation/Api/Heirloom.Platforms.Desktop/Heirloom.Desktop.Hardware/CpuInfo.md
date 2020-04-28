# Heirloom.Platforms.Desktop

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Platforms.Desktop][0]  

## CpuInfo

> **Namespace**: [Heirloom.Desktop.Hardware][0]  

Contains information related to the CPU.

```cs
public struct CpuInfo : IEquatable<CpuInfo>
```

### Inherits

IEquatable\<CpuInfo>

#### Properties

[Vendor][1], [Name][2], [ThreadCount][3], [ClockSpeed][4]

## Properties

| Name             | Summary                                                  |
|------------------|----------------------------------------------------------|
| [Vendor][1]      | Gets the CPU Vendor.                                     |
| [Name][2]        | Gets the name of the CPU.                                |
| [ThreadCount][3] | Gets how many threads (logical cores) this CPU supports. |
| [ClockSpeed][4]  | Gets the clockspeed of this CPU in MHz.                  |

[0]: ../../Heirloom.Platforms.Desktop.md
[1]: CpuInfo/Vendor.md
[2]: CpuInfo/Name.md
[3]: CpuInfo/ThreadCount.md
[4]: CpuInfo/ClockSpeed.md
