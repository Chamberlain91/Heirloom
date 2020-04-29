# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Shader Class

> **Namespace**: [Heirloom][0]  

Provides GLSL shader support for custom image effects and other visual processing.

```cs
public abstract class Shader : IDisposable
```

### Inherits

IDisposable

#### Properties

[Paths][1], [Uniforms][2]

#### Methods

[SetUniform][3], [SetUniform\<T>][4], [Dispose][5]

#### Static Properties

[Default][6]

## Properties

| Name          | Summary                                         |
|---------------|-------------------------------------------------|
| [Paths][1]    | The paths used to create this shader object.    |
| [Uniforms][2] | Enumerates the uniforms defined in this shader. |
| [Default][6]  | Gets the default (ie, "no effect") shader.      |

## Methods

| Name                | Summary                                     |
|---------------------|---------------------------------------------|
| [SetUniform][3]     |                                             |
| [SetUniform][3]     |                                             |
| [SetUniform][3]     |                                             |
| [SetUniform][3]     |                                             |
| [SetUniform\<T>][4] | Updates one of the shader uniforms by name. |
| [SetUniform][3]     | Updates one of the shader uniforms by name. |
| [Dispose][5]        |                                             |

[0]: ../../Heirloom.Core.md
[1]: Shader/Paths.md
[2]: Shader/Uniforms.md
[3]: Shader/SetUniform.md
[4]: Shader/SetUniform[T].md
[5]: Shader/Dispose.md
[6]: Shader/Default.md
