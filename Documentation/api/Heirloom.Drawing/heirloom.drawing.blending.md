# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Blending (Enum)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IComparable, IFormattable, IConvertible</small>  

Controls how drawing operations are blended into existing pixels.

### Values

#### Opaque
<member name="F:Heirloom.Drawing.Blending.Opaque">
  <summary>
            Drawn pixels are fully opaque and will replace existing pixels.
            </summary>
</member>

#### Alpha
<member name="F:Heirloom.Drawing.Blending.Alpha">
  <summary>
            Draw pixels are blended based on their alpha values with existing pixels.
            </summary>
</member>

#### Additive
<member name="F:Heirloom.Drawing.Blending.Additive">
  <summary>
            Drawn pixels are additively blended based on their alpha values with existing pixels.
            </summary>
</member>

#### Subtractive
<member name="F:Heirloom.Drawing.Blending.Subtractive">
  <summary>
            Drawn pixels are subtractively blended based on their alpha values with existing pixels.
            </summary>
</member>

#### Multiply
<member name="F:Heirloom.Drawing.Blending.Multiply">
  <summary>
            Drawn pixels are multiplicatively blended based on their alpha values with existing pixels.
            </summary>
</member>

#### Invert
<member name="F:Heirloom.Drawing.Blending.Invert">
  <summary>
            Drawn pixels act like an inversion filter with existing pixels.
            </summary>
</member>

