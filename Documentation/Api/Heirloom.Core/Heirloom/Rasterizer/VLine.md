# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Rasterizer.VLine (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Rasterizer][1]

### VLine(int, int, int)

Iterate over a perfectly vertical line.

```cs
public static IEnumerable<IntVector> VLine(int y1, int y2, int x)
```

`IteratorStateMachineAttribute`

| Name | Type  | Summary                  |
|------|-------|--------------------------|
| y1   | `int` | Line start y coordinate. |
| y2   | `int` | Line end y coordinate.   |
| x    | `int` | Line x coordinate.       |

> **Returns** - `IEnumerable\<IntVector>` - Generated points along the line.

[0]: ../../../Heirloom.Core.md
[1]: ../Rasterizer.md
