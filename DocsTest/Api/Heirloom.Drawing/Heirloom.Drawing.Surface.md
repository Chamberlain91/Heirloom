# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Surface (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Inherits**: [ImageSource](Heirloom.Drawing.ImageSource.md)</small>  

Represents a surface a [Graphics](Heirloom.Drawing.Graphics.md) object can draw on.

| Properties                 | Summary                                             |
|----------------------------|-----------------------------------------------------|
| [Size](#SIZ9C9392F9)       | Gets size of the surface in pixels.                 |
| [Width](#WID68924896)      | Gets the surface width in pixels.                   |
| [Height](#HEIE098AAEB)     | Gets the surface height in pixels.                  |
| [Multisample](#MULD8F2787) | Gets the multisampling quality set on this surface. |

### Constructors

#### Surface([IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) size, [MultisampleQuality](Heirloom.Drawing.MultisampleQuality.md) multisample = None)

Creates a new surface.

#### Surface(int width, int height, [MultisampleQuality](Heirloom.Drawing.MultisampleQuality.md) multisample = None)

Creates a new surface.

#### Surface(int width, int height, [MultisampleQuality](Heirloom.Drawing.MultisampleQuality.md) multisample, bool createNative)

### Properties

#### <a name="SIZ9C9392F9"></a>Size : [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md)


Gets size of the surface in pixels.

#### <a name="WID68924896"></a>Width : int

<small>`Read Only`</small>

Gets the surface width in pixels.

#### <a name="HEIE098AAEB"></a>Height : int

<small>`Read Only`</small>

Gets the surface height in pixels.

#### <a name="MULD8F2787"></a>Multisample : [MultisampleQuality](Heirloom.Drawing.MultisampleQuality.md)

<small>`Read Only`</small>

Gets the multisampling quality set on this surface.

