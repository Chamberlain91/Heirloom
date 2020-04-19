# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Surface (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</small>  
<small>**Inherits**: [ImageSource](Heirloom.Drawing.ImageSource.md)</small>  

Represents a surface a [Graphics](Heirloom.Drawing.Graphics.md) object can draw on.

| Properties                                     | Summary                                                            |
|------------------------------------------------|--------------------------------------------------------------------|
| [Size](#SIZ9C9392F9)                           | Gets size of the surface in pixels.                                |
| [Multisample](#MULD8F2787)                     | Gets the multisampling quality set on this surface.                |
| [IsScreenBound](#ISS465F8F76)                  | Determines if this surface is attached to a screen (ie, a window). |
| [MaxSupportedMultisampleQuality](#MAX50D0FBEA) | Gets the max multisample quality supported on this system.         |

### Constructors

#### Surface([IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) size, [MultisampleQuality](Heirloom.Drawing.MultisampleQuality.md) multisample = None)

Creates a new surface.

#### Surface(int width, int height, [MultisampleQuality](Heirloom.Drawing.MultisampleQuality.md) multisample = None)

Creates a new surface.

#### Surface(int width, int height, [MultisampleQuality](Heirloom.Drawing.MultisampleQuality.md) multisample, bool isScreenBound)

### Properties

#### <a name="SIZ9C9392F9"></a>Size : [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md)


Gets size of the surface in pixels.

#### <a name="MULD8F2787"></a>Multisample : [MultisampleQuality](Heirloom.Drawing.MultisampleQuality.md)

<small>`Read Only`</small>

Gets the multisampling quality set on this surface.

This wll be set to the value actually availble used to create the surface. Some platforms might not support all multisample levels.

#### <a name="ISS465F8F76"></a>IsScreenBound : bool

<small>`Read Only`</small>

Determines if this surface is attached to a screen (ie, a window).

#### <a name="MAX50D0FBEA"></a>MaxSupportedMultisampleQuality : [MultisampleQuality](Heirloom.Drawing.MultisampleQuality.md)

<small>`Static`, `Read Only`</small>

Gets the max multisample quality supported on this system.

