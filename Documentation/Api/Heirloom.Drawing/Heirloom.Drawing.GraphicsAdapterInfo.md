# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## GraphicsAdapterInfo (Struct)
<small>**Namespace**: Heirloom.Drawing</small>  
<small>`IsReadOnlyAttribute`</small>

| Fields                           | Summary                                                                                                |
|----------------------------------|--------------------------------------------------------------------------------------------------------|
| [IsMobilePlatform](#ISM8E791415) | Gets a value that determines if this application has been detected to be running on a mobile platform. |
| [Vendor](#VENA14B39A0)           | The adapter vedor (ie, NVIDIA or AMD).                                                                 |
| [Name](#NAM5943D12B)             | The adapter name (ie, GTX 1080).                                                                       |

### Fields

#### <a name="ISM8E791415"></a>IsMobilePlatform : bool
<small>`Read Only`</small>

Gets a value that determines if this application has been detected to be running on a mobile platform.

#### <a name="VENA14B39A0"></a>Vendor : string
<small>`Read Only`</small>

The adapter vedor (ie, NVIDIA or AMD).

#### <a name="NAM5943D12B"></a>Name : string
<small>`Read Only`</small>

The adapter name (ie, GTX 1080).

### Constructors

#### GraphicsAdapterInfo(bool isMobilePlatform, string vendor, string name)

