# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Rasterizer.CircleOutline (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Rasterizer][1]

### CircleOutline(int, int, int)

Rasterizes a circle outline.

```cs
public static IEnumerable<IntVector> CircleOutline(int cx, int cy, int r)
```

| Name | Type  | Summary                                |
|------|-------|----------------------------------------|
| cx   | `int` | The center x coordinate of the circle. |
| cy   | `int` | The center y coordinate of the circle. |
| r    | `int` | The radius of the circle.              |

> **Returns** - `IEnumerable<IntVector>` - Generated points on the shell of the circle.

[0]: ../../../Heirloom.Core.md
[1]: ../Rasterizer.md
