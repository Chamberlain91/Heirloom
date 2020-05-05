# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Rasterizer.Rectangle (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Rasterizer][1]

### Rectangle(int, int, int, int)

Rasterize a rectangular region.

```cs
public static IEnumerable<IntVector> Rectangle(int x, int y, int width, int height)
```

| Name   | Type  | Summary |
|--------|-------|---------|
| x      | `int` |         |
| y      | `int` |         |
| width  | `int` |         |
| height | `int` |         |

> **Returns** - `IEnumerable<IntVector>`

### Rectangle(IntRectangle)

Rasterize a rectangular region.

```cs
public static IEnumerable<IntVector> Rectangle(IntRectangle rect)
```

| Name | Type              | Summary |
|------|-------------------|---------|
| rect | [IntRectangle][2] |         |

> **Returns** - `IEnumerable<IntVector>`

### Rectangle(IntVector, IntSize)

Rasterize a rectangular region.

```cs
public static IEnumerable<IntVector> Rectangle(IntVector position, IntSize size)
```

| Name     | Type           | Summary |
|----------|----------------|---------|
| position | [IntVector][3] |         |
| size     | [IntSize][4]   |         |

> **Returns** - `IEnumerable<IntVector>`

### Rectangle(IntSize)

Rasterize a rectangular region.

```cs
public static IEnumerable<IntVector> Rectangle(IntSize size)
```

| Name | Type         | Summary |
|------|--------------|---------|
| size | [IntSize][4] |         |

> **Returns** - `IEnumerable<IntVector>`

[0]: ../../../Heirloom.Core.md
[1]: ../Rasterizer.md
[2]: ../IntRectangle.md
[3]: ../IntVector.md
[4]: ../IntSize.md
