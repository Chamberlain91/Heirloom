# Rasterizer.Rectangle

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Rasterizer][1]  

--------------------------------------------------------------------------------

### Rectangle(int, int, int, int)

Rasterize a rectangular region.

```cs
public IEnumerable<IntVector> Rectangle(int x, int y, int width, int height)
```

### Rectangle(IntRectangle)

Rasterize a rectangular region.

```cs
public IEnumerable<IntVector> Rectangle(IntRectangle rect)
```

### Rectangle(IntVector, IntSize)

Rasterize a rectangular region.

```cs
public IEnumerable<IntVector> Rectangle(IntVector position, IntSize size)
```

### Rectangle(IntSize)

Rasterize a rectangular region.

```cs
public IEnumerable<IntVector> Rectangle(IntSize size)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.Rasterizer.md
