# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## GraphicsCapabilities (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IEquatable\<GraphicsCapabilities></small>  
<small>`IsReadOnlyAttribute`</small>

| Fields                                     | Summary                                                                                                |
|--------------------------------------------|--------------------------------------------------------------------------------------------------------|
| [IsMobilePlatform](#ISM8E791415)           | Gets a value that determines if this application has been detected to be running on a mobile platform. |
| [MaxSupportedFragmentImages](#MAXA0D55BE4) |                                                                                                        |
| [MaxSupportedVertexImages](#MAXB719BE96)   |                                                                                                        |
| [MaxImageSize](#MAXF2A23E2C)               |                                                                                                        |
| [AdapterVendor](#ADA6819B9FF)              |                                                                                                        |
| [AdapterName](#ADAAF80D05A)                |                                                                                                        |

### Fields

#### <a name="ISM8E791415"></a>IsMobilePlatform : bool
<small>`Read Only`</small>

Gets a value that determines if this application has been detected to be running on a mobile platform.

#### <a name="MAXA0D55BE4"></a>MaxSupportedFragmentImages : int
<small>`Read Only`</small>

#### <a name="MAXB719BE96"></a>MaxSupportedVertexImages : int
<small>`Read Only`</small>

#### <a name="MAXF2A23E2C"></a>MaxImageSize : int
<small>`Read Only`</small>

#### <a name="ADA6819B9FF"></a>AdapterVendor : string
<small>`Read Only`</small>

#### <a name="ADAAF80D05A"></a>AdapterName : string
<small>`Read Only`</small>

### Constructors

#### GraphicsCapabilities(string adapterName, string adapterVendor, bool isMobilePlatform, int maxSupportedFragmentImages, int maxSupportedVertexImages, int maxImageSize)

