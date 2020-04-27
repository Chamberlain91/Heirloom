# IntRectangle.Include

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [IntRectangle][1]

--------------------------------------------------------------------------------

### Include(IntVector)

Mutates this rectangle to accommodate the given point.

```cs
public void Include(IntVector point)
```

Useful for computing a bounding rectangle.

### Include(in IntRectangle)

Mutates this rectangle to accommodate the given rectangle.

```cs
public void Include(in IntRectangle rect)
```

Useful for computing a bounding rectangle.

[0]: ../Heirloom.Core.md
[1]: Heirloom.IntRectangle.md
