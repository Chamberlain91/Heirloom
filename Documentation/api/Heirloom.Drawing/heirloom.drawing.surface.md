# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Surface (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Inherits**: [ImageSource](heirloom.drawing.imagesource.md)</small>  

Represents a surface a [Graphics](heirloom.drawing.graphics.md) object can draw on.

| Properties | Summary |
|------------|---------|
| [Size](#SIZ9C9392F9) | Gets size of the surface in pixels. |
| [Width](#WID68924896) | Gets the surface width in pixels. |
| [Height](#HEIE098AAEB) | Gets the surface height in pixels. |
| [Multisample](#MULD8F2787) | Gets the multisampling quality set on this surface. |

### Constructors

#### Surface([IntSize](../heirloom.math/heirloom.math.intsize.md) size, [MultisampleQuality](heirloom.drawing.multisamplequality.md) multisample = 1)

Creates a new surface.

#### Surface(int width, int height, [MultisampleQuality](heirloom.drawing.multisamplequality.md) multisample = 1)

Creates a new surface.

#### Surface(int width, int height, [MultisampleQuality](heirloom.drawing.multisamplequality.md) multisample, bool createNative)

### Properties

#### <a name="SIZ9C9392F9"></a>Size : [IntSize](../heirloom.math/heirloom.math.intsize.md)


Gets size of the surface in pixels.

#### <a name="WID68924896"></a>Width : int

<small>`Read Only`</small>

Gets the surface width in pixels.

#### <a name="HEIE098AAEB"></a>Height : int

<small>`Read Only`</small>

Gets the surface height in pixels.

#### <a name="MULD8F2787"></a>Multisample : [MultisampleQuality](heirloom.drawing.multisamplequality.md)

<small>`Read Only`</small>

Gets the multisampling quality set on this surface.

