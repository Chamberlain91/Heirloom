# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## DistortionShader (Sealed Class)
<small>**Namespace**: Examples.Shaders</small>  
<small>**Inherits**: [Shader](Heirloom.Drawing.Shader.md)</small>  
<small>**Interfaces**: IDisposable</small>  

Distortion shader.

| Properties                    | Summary                                                                                      |
|-------------------------------|----------------------------------------------------------------------------------------------|
| [DistortionMap](#DIS1DCAFCB7) | Gets or sets the distortion map. Only the RG channels are used and are remapped to -1 to +1. |
| [Offset](#OFF1FA8EDD)         | Gets or sets the offset applied to the distortion map (in uv coordinates).                   |
| [Strength](#STR7C69F4E5)      | Gets or sets the strength of the distortion (0.0 to 1.0, unclamped).                         |

### Constructors

#### DistortionShader([Image](Heirloom.Drawing.Image.md) distortionMap)

Constructs a new distortion shader.

### Properties

#### <a name="DIS1DCAFCB7"></a>DistortionMap : [Image](Heirloom.Drawing.Image.md)


Gets or sets the distortion map. Only the RG channels are used and are remapped to -1 to +1.

#### <a name="OFF1FA8EDD"></a>Offset : [Vector](../Heirloom.Math/Heirloom.Math.Vector.md)


Gets or sets the offset applied to the distortion map (in uv coordinates).

#### <a name="STR7C69F4E5"></a>Strength : float


Gets or sets the strength of the distortion (0.0 to 1.0, unclamped).

