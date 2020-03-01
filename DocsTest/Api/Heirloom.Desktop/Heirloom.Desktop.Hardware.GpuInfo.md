# Heirloom.Desktop

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md), [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## GpuInfo (Struct)
<small>**Namespace**: Heirloom.Desktop.Hardware</sub></small>  
<small>**Interfaces**: IEquatable\<GpuInfo></small>  

| Properties           | Summary                                                      |
|----------------------|--------------------------------------------------------------|
| [Vendor](#VENDA14B)  |                                                              |
| [Name](#NAME5943)    |                                                              |
| [Unknown](#UNKNA484) | Gets default information when properties of GPU are unknown. |

### Constructors

#### GpuInfo(string vendor, string name)

#### GpuInfo([GpuVendor](Heirloom.Desktop.Hardware.GpuVendor.md) vendor, string name)

### Properties

#### <a name="VENDA14B"></a> Vendor : [GpuVendor](Heirloom.Desktop.Hardware.GpuVendor.md)

<small>`Read Only`</small>

#### <a name="NAME5943"></a> Name : string

<small>`Read Only`</small>

#### <a name="UNKNA484"></a> Unknown : [GpuInfo](Heirloom.Desktop.Hardware.GpuInfo.md)

<small>`Static`</small>

Gets default information when properties of GPU are unknown.

