# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Shader (Class)

> **Namespace**: [Heirloom][0]

Provides GLSL shader support for custom image effects and other visual processing.

```cs
public abstract class Shader : IDisposable
```

### Inherits

IDisposable

### Properties

[Paths][1], [Uniforms][2]

### Methods

[Dispose][3], [SetUniform][4], [SetUniform\<T>][5]

### Static Properties

[Default][6]

## Properties

#### Instance

| Name          | Type                        | Summary                                         |
|---------------|-----------------------------|-------------------------------------------------|
| [Paths][1]    | `IReadOnlyList\<string>`    | The paths used to create this shader object.    |
| [Uniforms][2] | `IEnumerable\<UniformInfo>` | Enumerates the uniforms defined in this shader. |

#### Static

| Name         | Type        | Summary                                    |
|--------------|-------------|--------------------------------------------|
| [Default][6] | [Shader][7] | Gets the default (ie, "no effect") shader. |

## Methods

#### Instance

| Name                           | Return Type | Summary                                     |
|--------------------------------|-------------|---------------------------------------------|
| [Dispose()][3]                 | `void`      |                                             |
| [SetUniform(string, flo...][4] | `void`      |                                             |
| [SetUniform(string, int[])][4] | `void`      |                                             |
| [SetUniform(string, uin...][4] | `void`      |                                             |
| [SetUniform(string, boo...][4] | `void`      |                                             |
| [SetUniform(string, Ima...][4] | `void`      | Updates one of the shader uniforms by name. |
| [SetUniform<T>(string, T)][5]  | `void`      | Updates one of the shader uniforms by name. |

[0]: ../../Heirloom.Core.md
[1]: Shader/Paths.md
[2]: Shader/Uniforms.md
[3]: Shader/Dispose.md
[4]: Shader/SetUniform.md
[5]: Shader/SetUniform[T].md
[6]: Shader/Default.md
[7]: Shader.md
