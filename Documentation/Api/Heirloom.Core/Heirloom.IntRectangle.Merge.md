# IntRectangle.Merge

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [IntRectangle][1]

--------------------------------------------------------------------------------

### Merge(in IntRectangle, in IntRectangle)

Merges the given rectangles into one potentially larger rectangle.

```cs
public IntRectangle Merge(in IntRectangle a, in IntRectangle b)
```

Useful for computing a bounding rectangle.

### Merge(params IntRectangle[])

```cs
public IntRectangle Merge(params IntRectangle[] rects)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.IntRectangle.md
