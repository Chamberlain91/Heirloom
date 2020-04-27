# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Polygon.CreateStar

> **Namespace**: [Heirloom][0]  
> **Type**: [Polygon][1]  

### CreateStar(float)

Creates a polygon shaped like a standard 5 point star centered on the origin.

```cs
public static Polygon CreateStar(float radius)
```

### CreateStar(Vector, float)

Creates a polygon shaped like a standard 5 point star.

```cs
public static Polygon CreateStar(Vector center, float radius)
```

### CreateStar(int, float)

Creates a polygon shaped like a star centered on the origin.

```cs
public static Polygon CreateStar(int numPoints, float radius)
```

### CreateStar(Vector, int, float)

Creates a polygon shaped like a star.

```cs
public static Polygon CreateStar(Vector center, int numPoints, float radius)
```

### CreateStar(Vector, int, float, float)

Creates a polygon shaped like a star.

```cs
public static Polygon CreateStar(Vector center, int numPoints, float innerRadius, float outerRadius)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.Polygon.md
