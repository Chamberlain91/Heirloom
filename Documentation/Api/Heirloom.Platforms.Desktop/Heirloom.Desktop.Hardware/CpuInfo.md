# Heirloom.Platforms.Desktop

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Platforms.Desktop][0]

## CpuInfo (Struct)

> **Namespace**: [Heirloom.Desktop.Hardware][0]

Contains information related to the CPU.

```cs
public struct CpuInfo : IEquatable<CpuInfo>
```

`IsReadOnlyAttribute`

### Inherits

IEquatable\<CpuInfo>

### Properties

[ClockSpeed][1], [Name][2], [ThreadCount][3], [Vendor][4]

## Properties

#### Instance

| Name             | Type           | Summary                                                  |
|------------------|----------------|----------------------------------------------------------|
| [ClockSpeed][1]  | `int`          | Gets the clockspeed of this CPU in MHz.                  |
| [Name][2]        | `string`       | Gets the name of the CPU.                                |
| [ThreadCount][3] | `int`          | Gets how many threads (logical cores) this CPU supports. |
| [Vendor][4]      | [CpuVendor][5] | Gets the CPU Vendor.                                     |

[0]: ../../Heirloom.Platforms.Desktop.md
[1]: CpuInfo/ClockSpeed.md
[2]: CpuInfo/Name.md
[3]: CpuInfo/ThreadCount.md
[4]: CpuInfo/Vendor.md
[5]: CpuVendor.md
