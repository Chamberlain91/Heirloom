# Heirloom.Desktop

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md), [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## CpuInfo (Struct)
<small>**Namespace**: Heirloom.Desktop.Hardware</sub></small>  
<small>**Interfaces**: IEquatable\<CpuInfo></small>  

| Properties               | Summary                                                      |
|--------------------------|--------------------------------------------------------------|
| [Vendor](#VENDA14B)      |                                                              |
| [Name](#NAME5943)        |                                                              |
| [ThreadCount](#THRE4107) |                                                              |
| [ClockSpeed](#CLOCF5A0)  |                                                              |
| [Unknown](#UNKNA484)     | Gets default information when properties of CPU are unknown. |

### Constructors

#### CpuInfo(string name, int clockSpeed, int threadCount)

#### CpuInfo([CpuVendor](Heirloom.Desktop.Hardware.CpuVendor.md) vendor, string name, int clockSpeed, int threadCount)

### Properties

#### <a name="VENDA14B"></a> Vendor : [CpuVendor](Heirloom.Desktop.Hardware.CpuVendor.md)

<small>`Read Only`</small>

#### <a name="NAME5943"></a> Name : string

<small>`Read Only`</small>

#### <a name="THRE4107"></a> ThreadCount : int

<small>`Read Only`</small>

#### <a name="CLOCF5A0"></a> ClockSpeed : int

<small>`Read Only`</small>

#### <a name="UNKNA484"></a> Unknown : [CpuInfo](Heirloom.Desktop.Hardware.CpuInfo.md)

<small>`Static`</small>

Gets default information when properties of CPU are unknown.

