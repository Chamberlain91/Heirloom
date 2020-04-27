# Rectangle.Transform

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Rectangle][1]

--------------------------------------------------------------------------------

### Transform(in Matrix)

Transforms the four corners of this rectangle and updates itself to bound these points.

```cs
public Rectangle Transform(in Matrix matrix)
```

### Transform(Rectangle, in Matrix)

Transforms the four corners of this rectangle and returns the bounding rectangle of these points.

```cs
public Rectangle Transform(Rectangle rectangle, in Matrix matrix)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.Rectangle.md
