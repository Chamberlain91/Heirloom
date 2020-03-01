# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## TextDrawState (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IEquatable\<TextDrawState></small>  

Represents information of any particular glyph when drawing text.

| Fields                    | Summary                                                                                                         |
|---------------------------|-----------------------------------------------------------------------------------------------------------------|
| [Transform](#TRA97DF3302) | The relative transform to apply to the current glyph image (set to `Heirloom.Math.Matrix.Identity` by default). |
| [Position](#POSF46C3C91)  | The position of top left corner of the current glyph image.                                                     |
| [Color](#COLD1229651)     | The color of the current glyph.                                                                                 |

### Fields

#### <a name="TRA97DF3302"></a>Transform : [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md)

The relative transform to apply to the current glyph image (set to `Heirloom.Math.Matrix.Identity` by default).

#### <a name="POSF46C3C91"></a>Position : [Vector](../Heirloom.Math/Heirloom.Math.Vector.md)

The position of top left corner of the current glyph image.

#### <a name="COLD1229651"></a>Color : [Color](Heirloom.Drawing.Color.md)

The color of the current glyph.

