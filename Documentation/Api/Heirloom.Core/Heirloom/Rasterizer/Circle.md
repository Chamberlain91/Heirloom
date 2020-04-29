# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Rasterizer.Circle (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Rasterizer][1]

### Circle(int, int, int)

Rasterizes a filled circle.

```cs
public static IEnumerable<IntVector> Circle(int cx, int cy, int r)
```

`IteratorStateMachineAttribute`

| Name | Type  | Summary                                |
|------|-------|----------------------------------------|
| cx   | `int` | The center x coordinate of the circle. |
| cy   | `int` | The center y coordinate of the circle. |
| r    | `int` | The radius of the circle.              |

> **Returns** - `IEnumerable\<IntVector>` - Generated points within the circle.

[0]: ../../../Heirloom.Core.md
[1]: ../Rasterizer.md
