# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Rectangle.Merge

> **Namespace**: [Heirloom][0]  
> **Type**: [Rectangle][1]  

### Merge(in Rectangle, in Rectangle)

Merges the given rectangles into one potentially larger rectangle.

```cs
public static Rectangle Merge(in Rectangle a, in Rectangle b)
```

Useful for computing a bounding rectangle.

### Merge(params Rectangle[])

```cs
public static Rectangle Merge(params Rectangle[] rects)
```

[0]: ../../../Heirloom.Core.md
[1]: ../Rectangle.md
