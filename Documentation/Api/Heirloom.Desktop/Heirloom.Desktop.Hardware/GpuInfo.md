# Heirloom.Desktop

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Desktop][0]

## GpuInfo (Struct)

> **Namespace**: [Heirloom.Desktop.Hardware][0]

Represents information about the GPU on some device.

```cs
public struct GpuInfo : IEquatable<GpuInfo>
```

`IsReadOnlyAttribute`

### Inherits

IEquatable\<GpuInfo>

### Properties

[Name][1], [Vendor][2]

### Methods

[Equals][3], [GetHashCode][4], [ToString][5]

## Properties

#### Instance

| Name        | Type           | Summary                     |
|-------------|----------------|-----------------------------|
| [Name][1]   | `string`       | Gets the name of the GPU.   |
| [Vendor][2] | [GpuVendor][6] | Gets the vendor of the GPU. |

## Methods

#### Instance

| Name                 | Return Type | Summary                                               |
|----------------------|-------------|-------------------------------------------------------|
| [Equals(object)][3]  | `bool`      |                                                       |
| [Equals(GpuInfo)][3] | `bool`      | Compares the GpuInfo against each other for equality. |
| [GetHashCode()][4]   | `int`       |                                                       |
| [ToString()][5]      | `string`    | Returns a string representation of the GpuInfo .      |

## Operators

| Name                           | Return Type | Summary                              |
|--------------------------------|-------------|--------------------------------------|
| [Equality(GpuInfo, GpuI...][7] | `bool`      | Compares two GpuInfo for equality.   |
| [Inequality(GpuInfo, Gp...][8] | `bool`      | Compares two GpuInfo for inequality. |

[0]: ../../Heirloom.Desktop.md
[1]: GpuInfo/Name.md
[2]: GpuInfo/Vendor.md
[3]: GpuInfo/Equals.md
[4]: GpuInfo/GetHashCode.md
[5]: GpuInfo/ToString.md
[6]: GpuVendor.md
[7]: GpuInfo/op_Equality.md
[8]: GpuInfo/op_Inequality.md
