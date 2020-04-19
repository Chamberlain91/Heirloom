# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## VectorBlurShader (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</small>  
<small>**Inherits**: [Shader](Heirloom.Drawing.Shader.md)</small>  
<small>**Interfaces**: IDisposable</small>  

Vector blur shader.

| Properties             | Summary                                                                                          |
|------------------------|--------------------------------------------------------------------------------------------------|
| [Vector](#VEC7CD94BFD) | Gets or sets the blur vector. Strength of the blur is determined by the magnitude of the vector. |

### Constructors

#### VectorBlurShader(int quality)

Constructs a new blur shader.

### Properties

#### <a name="VEC7CD94BFD"></a>Vector : [Vector](../Heirloom.Math/Heirloom.Math.Vector.md)


Gets or sets the blur vector. Strength of the blur is determined by the magnitude of the vector.

