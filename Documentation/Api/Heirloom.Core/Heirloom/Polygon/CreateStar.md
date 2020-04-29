# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Polygon.CreateStar (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Polygon][1]

### CreateStar(float)

Creates a polygon shaped like a standard 5 point star centered on the origin.

```cs
public static Polygon CreateStar(float radius)
```

| Name   | Type    | Summary |
|--------|---------|---------|
| radius | `float` |         |

> **Returns** - [Polygon][1]

### CreateStar(Vector, float)

Creates a polygon shaped like a standard 5 point star.

```cs
public static Polygon CreateStar(Vector center, float radius)
```

| Name   | Type        | Summary |
|--------|-------------|---------|
| center | [Vector][2] |         |
| radius | `float`     |         |

> **Returns** - [Polygon][1]

### CreateStar(int, float)

Creates a polygon shaped like a star centered on the origin.

```cs
public static Polygon CreateStar(int numPoints, float radius)
```

| Name      | Type    | Summary |
|-----------|---------|---------|
| numPoints | `int`   |         |
| radius    | `float` |         |

> **Returns** - [Polygon][1]

### CreateStar(Vector, int, float)

Creates a polygon shaped like a star.

```cs
public static Polygon CreateStar(Vector center, int numPoints, float radius)
```

| Name      | Type        | Summary |
|-----------|-------------|---------|
| center    | [Vector][2] |         |
| numPoints | `int`       |         |
| radius    | `float`     |         |

> **Returns** - [Polygon][1]

### CreateStar(Vector, int, float, float)

Creates a polygon shaped like a star.

```cs
public static Polygon CreateStar(Vector center, int numPoints, float innerRadius, float outerRadius)
```

| Name        | Type        | Summary |
|-------------|-------------|---------|
| center      | [Vector][2] |         |
| numPoints   | `int`       |         |
| innerRadius | `float`     |         |
| outerRadius | `float`     |         |

> **Returns** - [Polygon][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Polygon.md
[2]: ../Vector.md
