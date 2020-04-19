# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## RepeatMode (Enum)
<small>**Namespace**: Heirloom.Drawing</small>  
<small>**Interfaces**: IComparable, IFormattable, IConvertible</small>  

Represents the behaviour when sampling an image outside its natural bounds.

### Values

#### Blank
<member name="F:Heirloom.Drawing.RepeatMode.Blank">
  <summary>
            Sampling coordinates outside image return transparent black.
            </summary>
</member>

#### Repeat
<member name="F:Heirloom.Drawing.RepeatMode.Repeat">
  <summary>
            Sampling coordinates outside image bounds cause the image to be repeated.
            </summary>
</member>

#### Clamp
<member name="F:Heirloom.Drawing.RepeatMode.Clamp">
  <summary>
            Sampling coordinates are clamped to image bounds.
            </summary>
</member>

