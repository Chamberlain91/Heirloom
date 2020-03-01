# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## GraphicsCapabilities (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IEquatable\<GraphicsCapabilities></small>  

| Fields                                  | Summary                                                                                                |
|-----------------------------------------|--------------------------------------------------------------------------------------------------------|
| [IsMobilePlatform](#ISMO8E79)           | Gets a value that determines if this application has been detected to be running on a mobile platform. |
| [MaxSupportedFragmentImages](#MAXSA0D5) |                                                                                                        |
| [MaxSupportedVertexImages](#MAXSB719)   |                                                                                                        |
| [MaxImageSize](#MAXIF2A2)               |                                                                                                        |
| [AdapterVendor](#ADAP6819)              |                                                                                                        |
| [AdapterName](#ADAPAF80)                |                                                                                                        |

### Fields

#### <a name="ISMO8E79"></a> IsMobilePlatform : bool
<small>`Read Only`</small>

Gets a value that determines if this application has been detected to be running on a mobile platform.

#### <a name="MAXSA0D5"></a> MaxSupportedFragmentImages : int
<small>`Read Only`</small>

#### <a name="MAXSB719"></a> MaxSupportedVertexImages : int
<small>`Read Only`</small>

#### <a name="MAXIF2A2"></a> MaxImageSize : int
<small>`Read Only`</small>

#### <a name="ADAP6819"></a> AdapterVendor : string
<small>`Read Only`</small>

#### <a name="ADAPAF80"></a> AdapterName : string
<small>`Read Only`</small>

### Constructors

#### GraphicsCapabilities(string adapterName, string adapterVendor, bool isMobilePlatform, int maxSupportedFragmentImages, int maxSupportedVertexImages, int maxImageSize)

