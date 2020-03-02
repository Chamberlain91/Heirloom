# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## ImageSource (Abstract Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

| Properties                    | Summary                                                         |
|-------------------------------|-----------------------------------------------------------------|
| [Size](#SIZ9C9392F9)          | The size of this image.                                         |
| [Origin](#ORI85E4C2C0)        | The offset used to 'center' the image around a non-zero origin. |
| [Version](#VERFB25B632)       | Version number to track changes against the image data.         |
| [Interpolation](#INT9DB69D30) | Interpolation mode.                                             |
| [Repeat](#REP237F223B)        | Repeat mode.                                                    |

### Constructors

#### ImageSource()

### Properties

#### <a name="SIZ9C9392F9"></a>Size : [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md)


The size of this image.

#### <a name="ORI85E4C2C0"></a>Origin : [Vector](../Heirloom.Math/Heirloom.Math.Vector.md)


The offset used to 'center' the image around a non-zero origin.

#### <a name="VERFB25B632"></a>Version : uint


Version number to track changes against the image data.

#### <a name="INT9DB69D30"></a>Interpolation : [InterpolationMode](Heirloom.Drawing.InterpolationMode.md)


Interpolation mode.

#### <a name="REP237F223B"></a>Repeat : [RepeatMode](Heirloom.Drawing.RepeatMode.md)


Repeat mode.
