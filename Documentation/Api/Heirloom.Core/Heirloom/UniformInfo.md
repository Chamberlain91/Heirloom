# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## UniformInfo (Class)

> **Namespace**: [Heirloom][0]

Contains information of a uniform from a [Shader][1] .

```cs
public sealed class UniformInfo
```

### Properties

[ArraySize][2], [Dimensions][3], [IsArray][4], [IsMatrix][5], [IsVector][6], [Name][7], [Type][8]

## Properties

#### Instance

| Name            | Type              | Summary                         |
|-----------------|-------------------|---------------------------------|
| [ArraySize][2]  | `int`             | The array size of this uniform. |
| [Dimensions][3] | [IntSize][9]      | The dimensions of this uniform. |
| [IsArray][4]    | `bool`            | Is this uniform an array?       |
| [IsMatrix][5]   | `bool`            | Is this uniform a matrix?       |
| [IsVector][6]   | `bool`            | Is this uniform a vector?       |
| [Name][7]       | `string`          | The name of this uniform.       |
| [Type][8]       | [UniformType][10] | The type of this uniform.       |

[0]: ../../Heirloom.Core.md
[1]: Shader.md
[2]: UniformInfo/ArraySize.md
[3]: UniformInfo/Dimensions.md
[4]: UniformInfo/IsArray.md
[5]: UniformInfo/IsMatrix.md
[6]: UniformInfo/IsVector.md
[7]: UniformInfo/Name.md
[8]: UniformInfo/Type.md
[9]: IntSize.md
[10]: UniformType.md
