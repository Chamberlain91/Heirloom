# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## ImageSource (Abstract Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

| Properties                 | Summary                                                         |
|----------------------------|-----------------------------------------------------------------|
| [Size](#SIZE9C93)          | The size of this image.                                         |
| [Origin](#ORIG85E4)        | The offset used to 'center' the image around a non-zero origin. |
| [Version](#VERSFB25)       | Version number to track changes against the image data.         |
| [Interpolation](#INTE9DB6) | Interpolation mode.                                             |
| [Repeat](#REPE237F)        | Repeat mode.                                                    |

### Constructors

#### ImageSource()

### Properties

#### <a name="SIZE9C93"></a> Size : [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md)


The size of this image.

#### <a name="ORIG85E4"></a> Origin : [Vector](../Heirloom.Math/Heirloom.Math.Vector.md)


The offset used to 'center' the image around a non-zero origin.

#### <a name="VERSFB25"></a> Version : uint


Version number to track changes against the image data.

#### <a name="INTE9DB6"></a> Interpolation : [InterpolationMode](Heirloom.Drawing.InterpolationMode.md)


Interpolation mode.

#### <a name="REPE237F"></a> Repeat : [RepeatMode](Heirloom.Drawing.RepeatMode.md)


Repeat mode.

