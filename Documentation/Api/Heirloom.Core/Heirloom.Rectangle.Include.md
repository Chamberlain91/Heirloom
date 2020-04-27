# Rectangle.Include

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Rectangle][1]  

--------------------------------------------------------------------------------

### Include(Vector)

Mutates this rectangle to accommodate the given point.

```cs
public void Include(Vector point)
```

Useful for computing a bounding rectangle.

### Include(in Rectangle)

Mutates this rectangle to accommodate the given rectangle.

```cs
public void Include(in Rectangle rect)
```

Useful for computing a bounding rectangle.

[0]: ../Heirloom.Core.md
[1]: Heirloom.Rectangle.md
