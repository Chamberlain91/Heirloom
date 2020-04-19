# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## NineSlice (Class)
<small>**Namespace**: Heirloom.Drawing</small>  

A special stretchable rectangle of an image.

| Fields                 | Summary                                                                                 |
|------------------------|-----------------------------------------------------------------------------------------|
| [Image](#IMA742F84D3)  | The image used by this nine slice.                                                      |
| [Center](#CEN7CD91D4B) | The center rectangle of the nine slice. This implicitly defines all other slice bounds. |

### Fields

#### <a name="IMA742F84D3"></a>Image : [Image](Heirloom.Drawing.Image.md)

The image used by this nine slice.

#### <a name="CEN7CD91D4B"></a>Center : [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md)

The center rectangle of the nine slice. This implicitly defines all other slice bounds.

### Constructors

#### NineSlice([Image](Heirloom.Drawing.Image.md) frame, [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) center)

Constructs a new nine slice.

