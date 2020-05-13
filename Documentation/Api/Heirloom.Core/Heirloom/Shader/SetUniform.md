# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Shader.SetUniform (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Shader][1]

### SetUniform(string, float[])

```cs
protected void SetUniform(string name, float[] values)
```

| Name   | Type      | Summary |
|--------|-----------|---------|
| name   | `string`  |         |
| values | `float[]` |         |

> **Returns** - `void`

### SetUniform(string, int[])

```cs
protected void SetUniform(string name, int[] values)
```

| Name   | Type     | Summary |
|--------|----------|---------|
| name   | `string` |         |
| values | `int[]`  |         |

> **Returns** - `void`

### SetUniform(string, uint[])

```cs
protected void SetUniform(string name, uint[] values)
```

| Name   | Type     | Summary |
|--------|----------|---------|
| name   | `string` |         |
| values | `uint[]` |         |

> **Returns** - `void`

### SetUniform(string, bool[])

```cs
protected void SetUniform(string name, bool[] values)
```

| Name   | Type     | Summary |
|--------|----------|---------|
| name   | `string` |         |
| values | `bool[]` |         |

> **Returns** - `void`

### SetUniform(string, ImageSource)

Updates one of the shader uniforms by name.

```cs
protected void SetUniform(string name, ImageSource image)
```

| Name  | Type             | Summary                            |
|-------|------------------|------------------------------------|
| name  | `string`         | The name of the uniform.           |
| image | [ImageSource][2] | An image to assign to the uniform. |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Shader.md
[2]: ../ImageSource.md
