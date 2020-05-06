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

[Dispose][3], [Finalize][4], [SetUniform][5], [SetUniform\<T>][6]

### Static Properties

[Default][7]

## Properties

#### Instance

| Name          | Type                       | Summary                                         |
|---------------|----------------------------|-------------------------------------------------|
| [Paths][1]    | `IReadOnlyList<string>`    | The paths used to create this shader object.    |
| [Uniforms][2] | `IEnumerable<UniformInfo>` | Enumerates the uniforms defined in this shader. |

#### Static

| Name         | Type        | Summary                                    |
|--------------|-------------|--------------------------------------------|
| [Default][7] | [Shader][8] | Gets the default (ie, "no effect") shader. |

## Methods

#### Instance

| Name                           | Return Type | Summary                                     |
|--------------------------------|-------------|---------------------------------------------|
| [Dispose()][3]                 | `void`      |                                             |
| [Finalize()][4]                | `void`      |                                             |
| [SetUniform(string, flo...][5] | `void`      |                                             |
| [SetUniform(string, int[])][5] | `void`      |                                             |
| [SetUniform(string, uin...][5] | `void`      |                                             |
| [SetUniform(string, boo...][5] | `void`      |                                             |
| [SetUniform(string, Ima...][5] | `void`      | Updates one of the shader uniforms by name. |
| [SetUniform<T>(string, T)][6]  | `void`      | Updates one of the shader uniforms by name. |

[0]: ../../Heirloom.Core.md
[1]: Shader/Paths.md
[2]: Shader/Uniforms.md
[3]: Shader/Dispose.md
[4]: Shader/Finalize.md
[5]: Shader/SetUniform.md
[6]: Shader/SetUniform[T].md
[7]: Shader/Default.md
[8]: Shader.md
