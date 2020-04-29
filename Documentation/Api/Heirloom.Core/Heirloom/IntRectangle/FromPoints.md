# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IntRectangle.FromPoints (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [IntRectangle][1]

### FromPoints(params IntVector[])

```cs
public static IntRectangle FromPoints(params IntVector[] points)
```

| Name   | Type             | Summary |
|--------|------------------|---------|
| points | [IntVector[]][2] |         |

> **Returns** - [IntRectangle][1]

### FromPoints(IEnumerable<IntVector>)

Computes the bounding rectangle of the given set of points.

```cs
public static IntRectangle FromPoints(IEnumerable<IntVector> points)
```

| Name   | Type                      | Summary |
|--------|---------------------------|---------|
| points | `IEnumerable\<IntVector>` |         |

> **Returns** - [IntRectangle][1]

[0]: ../../../Heirloom.Core.md
[1]: ../IntRectangle.md
[2]: ../IntVector.md
