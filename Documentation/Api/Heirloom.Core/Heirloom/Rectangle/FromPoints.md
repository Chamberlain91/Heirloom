# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Rectangle.FromPoints (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Rectangle][1]

### FromPoints(params Vector[])

```cs
public static Rectangle FromPoints(params Vector[] points)
```

| Name   | Type          | Summary |
|--------|---------------|---------|
| points | [Vector[]][2] |         |

> **Returns** - [Rectangle][1]

### FromPoints(IEnumerable<Vector>)

Computes the bounding rectangle of the given set of points.

```cs
public static Rectangle FromPoints(IEnumerable<Vector> points)
```

| Name   | Type                  | Summary |
|--------|-----------------------|---------|
| points | `IEnumerable<Vector>` |         |

> **Returns** - [Rectangle][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Rectangle.md
[2]: ../Vector.md
