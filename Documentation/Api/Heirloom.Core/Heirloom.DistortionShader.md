# DistortionShader

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Distortion shader.

```cs
public sealed class DistortionShader : Shader, IDisposable
```

--------------------------------------------------------------------------------

**Inherits**: [Shader][1], IDisposable

**Properties**: [DistortionMap][2], [Offset][3], [Strength][4]

--------------------------------------------------------------------------------

## Properties

| Name               | Summary                                                                                      |
|--------------------|----------------------------------------------------------------------------------------------|
| [DistortionMap][2] | Gets or sets the distortion map. Only the RG channels are used and are remapped to -1 to +1. |
| [Offset][3]        | Gets or sets the offset applied to the distortion map (in uv coordinates).                   |
| [Strength][4]      | Gets or sets the strength of the distortion (0.0 to 1.0, unclamped).                         |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Shader.md
[2]: Heirloom.DistortionShader.DistortionMap.md
[3]: Heirloom.DistortionShader.Offset.md
[4]: Heirloom.DistortionShader.Strength.md
