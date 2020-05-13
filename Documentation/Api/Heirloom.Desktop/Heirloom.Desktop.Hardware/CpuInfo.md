# Heirloom.Desktop

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Desktop][0]

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

### Methods

[Equals][5], [GetHashCode][6], [ToString][7]

## Properties

#### Instance

| Name             | Type           | Summary                                                  |
|------------------|----------------|----------------------------------------------------------|
| [ClockSpeed][1]  | `int`          | Gets the clockspeed of this CPU in MHz.                  |
| [Name][2]        | `string`       | Gets the name of the CPU.                                |
| [ThreadCount][3] | `int`          | Gets how many threads (logical cores) this CPU supports. |
| [Vendor][4]      | [CpuVendor][8] | Gets the CPU Vendor.                                     |

## Methods

#### Instance

| Name                 | Return Type | Summary                                               |
|----------------------|-------------|-------------------------------------------------------|
| [Equals(object)][5]  | `bool`      |                                                       |
| [Equals(CpuInfo)][5] | `bool`      | Compares the CpuInfo against each other for equality. |
| [GetHashCode()][6]   | `int`       |                                                       |
| [ToString()][7]      | `string`    | Returns a string representation of the CpuInfo .      |

## Operators

| Name                            | Return Type | Summary                              |
|---------------------------------|-------------|--------------------------------------|
| [Equality(CpuInfo, CpuI...][9]  | `bool`      | Compares two CpuInfo for equality.   |
| [Inequality(CpuInfo, Cp...][10] | `bool`      | Compares two CpuInfo for inequality. |

[0]: ../../Heirloom.Desktop.md
[1]: CpuInfo/ClockSpeed.md
[2]: CpuInfo/Name.md
[3]: CpuInfo/ThreadCount.md
[4]: CpuInfo/Vendor.md
[5]: CpuInfo/Equals.md
[6]: CpuInfo/GetHashCode.md
[7]: CpuInfo/ToString.md
[8]: CpuVendor.md
[9]: CpuInfo/op_Equality.md
[10]: CpuInfo/op_Inequality.md
