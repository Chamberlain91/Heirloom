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

## Properties

#### Instance

| Name        | Type           | Summary                     |
|-------------|----------------|-----------------------------|
| [Name][1]   | `string`       | Gets the name of the GPU.   |
| [Vendor][2] | [GpuVendor][3] | Gets the vendor of the GPU. |

[0]: ../../Heirloom.Desktop.md
[1]: GpuInfo/Name.md
[2]: GpuInfo/Vendor.md
[3]: GpuVendor.md
