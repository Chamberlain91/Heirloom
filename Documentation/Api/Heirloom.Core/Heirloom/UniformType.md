# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## UniformType

> **Namespace**: [Heirloom][0]  

Represents the type of a uniform in a [Shader][1] .

```cs
public enum UniformType : IComparable, IFormattable, IConvertible
```

| Name            | Summary                                      |
|-----------------|----------------------------------------------|
| Float           | The uniform is a float type.                 |
| Integer         | The uniform is a integer type.               |
| UnsignedInteger | The uniform is a unsigned integer type.      |
| Bool            | The uniform is a boolean type.               |
| Image           | The uniform is a image (ie, sampler2D) type. |

[0]: ../../Heirloom.Core.md
[1]: Shader.md
