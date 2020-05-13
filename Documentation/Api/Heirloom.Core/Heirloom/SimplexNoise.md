# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## SimplexNoise (Class)

> **Namespace**: [Heirloom][0]

Implements methods for sampling 2D and 3D simplex noise.

```cs
public class SimplexNoise : INoise2D, INoise3D
```

### Inherits

[INoise2D][1], [INoise3D][2]

### Methods

[Sample][3]

## Methods

#### Instance

| Name                           | Return Type | Summary                         |
|--------------------------------|-------------|---------------------------------|
| [Sample(float, float)][3]      | `float`     | Sample two-dimensional noise.   |
| [Sample(float, float, f...][3] | `float`     | Sample three-dimensional noise. |

[0]: ../../Heirloom.Core.md
[1]: INoise2D.md
[2]: INoise3D.md
[3]: SimplexNoise/Sample.md
