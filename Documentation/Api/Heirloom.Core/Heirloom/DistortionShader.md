# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## DistortionShader (Class)

> **Namespace**: [Heirloom][0]

Distortion shader.

```cs
public sealed class DistortionShader : Shader, IDisposable
```

### Inherits

[Shader][1], IDisposable

### Properties

[DistortionMap][2], [Offset][3], [Strength][4]

## Properties

#### Instance

| Name               | Type        | Summary                                                                |
|--------------------|-------------|------------------------------------------------------------------------|
| [DistortionMap][2] | [Image][5]  | Gets or sets the distortion map. Only the RG channels are used and ... |
| [Offset][3]        | [Vector][6] | Gets or sets the offset applied to the distortion map (in uv coordi... |
| [Strength][4]      | `float`     | Gets or sets the strength of the distortion (0.0 to 1.0, unclamped).   |

[0]: ../../Heirloom.Core.md
[1]: Shader.md
[2]: DistortionShader/DistortionMap.md
[3]: DistortionShader/Offset.md
[4]: DistortionShader/Strength.md
[5]: Image.md
[6]: Vector.md
