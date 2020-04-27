# Rectangle.Merge

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Rectangle][1]

--------------------------------------------------------------------------------

### Merge(in Rectangle, in Rectangle)

Merges the given rectangles into one potentially larger rectangle.

```cs
public Rectangle Merge(in Rectangle a, in Rectangle b)
```

Useful for computing a bounding rectangle.

### Merge(params Rectangle[])

```cs
public Rectangle Merge(params Rectangle[] rects)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.Rectangle.md
