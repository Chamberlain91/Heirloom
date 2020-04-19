# Heirloom.Desktop

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md), [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## CpuInfo (Struct)
<small>**Namespace**: Heirloom.Desktop.Hardware</small>  
<small>**Interfaces**: IEquatable\<CpuInfo></small>  
<small>`IsReadOnlyAttribute`</small>

| Properties                  | Summary                                                      |
|-----------------------------|--------------------------------------------------------------|
| [Vendor](#VENA14B39A0)      |                                                              |
| [Name](#NAM5943D12B)        |                                                              |
| [ThreadCount](#THR4107A6E1) |                                                              |
| [ClockSpeed](#CLOF5A035AF)  |                                                              |
| [Unknown](#UNKA4848C14)     | Gets default information when properties of CPU are unknown. |

### Constructors

#### CpuInfo(string name, int clockSpeed, int threadCount)

#### CpuInfo([CpuVendor](Heirloom.Desktop.Hardware.CpuVendor.md) vendor, string name, int clockSpeed, int threadCount)

### Properties

#### <a name="VENA14B39A0"></a>Vendor : [CpuVendor](Heirloom.Desktop.Hardware.CpuVendor.md)

<small>`Read Only`</small>

#### <a name="NAM5943D12B"></a>Name : string

<small>`Read Only`</small>

#### <a name="THR4107A6E1"></a>ThreadCount : int

<small>`Read Only`</small>

#### <a name="CLOF5A035AF"></a>ClockSpeed : int

<small>`Read Only`</small>

#### <a name="UNKA4848C14"></a>Unknown : [CpuInfo](Heirloom.Desktop.Hardware.CpuInfo.md)

<small>`Static`</small>

Gets default information when properties of CPU are unknown.

