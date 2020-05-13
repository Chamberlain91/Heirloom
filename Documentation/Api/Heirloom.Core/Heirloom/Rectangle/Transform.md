# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Rectangle.Transform (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Rectangle][1]

### Transform(in Matrix)

Transforms the four corners of this rectangle and updates itself to bound these points.

```cs
public Rectangle Transform(in Matrix matrix)
```

| Name   | Type        | Summary |
|--------|-------------|---------|
| matrix | [Matrix][2] |         |

> **Returns** - [Rectangle][1]

### Transform(Rectangle, in Matrix)

Transforms the four corners of this rectangle and returns the bounding rectangle of these points.

```cs
public static Rectangle Transform(Rectangle rectangle, in Matrix matrix)
```

| Name      | Type           | Summary |
|-----------|----------------|---------|
| rectangle | [Rectangle][1] |         |
| matrix    | [Matrix][2]    |         |

> **Returns** - [Rectangle][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Rectangle.md
[2]: ../Matrix.md
