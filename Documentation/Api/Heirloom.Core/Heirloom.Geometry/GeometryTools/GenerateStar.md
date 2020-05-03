# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GeometryTools.GenerateStar (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [GeometryTools][1]

### GenerateStar(float)

Generates the points of a common five-pointed star.

```cs
public static IEnumerable<Vector> GenerateStar(float radius)
```

| Name   | Type    | Summary |
|--------|---------|---------|
| radius | `float` |         |

> **Returns** - `IEnumerable\<Vector>`

### GenerateStar(Vector, float)

Generates the points of a common five-pointed star.

```cs
public static IEnumerable<Vector> GenerateStar(Vector center, float radius)
```

| Name   | Type        | Summary |
|--------|-------------|---------|
| center | [Vector][2] |         |
| radius | `float`     |         |

> **Returns** - `IEnumerable\<Vector>`

### GenerateStar(int, float)

Generates the points of a star.

```cs
public static IEnumerable<Vector> GenerateStar(int numPoints, float radius)
```

| Name      | Type    | Summary |
|-----------|---------|---------|
| numPoints | `int`   |         |
| radius    | `float` |         |

> **Returns** - `IEnumerable\<Vector>`

### GenerateStar(Vector, int, float)

Generates the points of a star.

```cs
public static IEnumerable<Vector> GenerateStar(Vector center, int numPoints, float radius)
```

| Name      | Type        | Summary |
|-----------|-------------|---------|
| center    | [Vector][2] |         |
| numPoints | `int`       |         |
| radius    | `float`     |         |

> **Returns** - `IEnumerable\<Vector>`

### GenerateStar(int, float, float)

Generates the points of a star.

```cs
public static IEnumerable<Vector> GenerateStar(int numPoints, float innerRadius, float outerRadius)
```

| Name        | Type    | Summary |
|-------------|---------|---------|
| numPoints   | `int`   |         |
| innerRadius | `float` |         |
| outerRadius | `float` |         |

> **Returns** - `IEnumerable\<Vector>`

### GenerateStar(Vector, int, float, float)

Generates the points of a star.

```cs
public static IEnumerable<Vector> GenerateStar(Vector center, int numPoints, float innerRadius, float outerRadius)
```

`IteratorStateMachineAttribute`

| Name        | Type        | Summary |
|-------------|-------------|---------|
| center      | [Vector][2] |         |
| numPoints   | `int`       |         |
| innerRadius | `float`     |         |
| outerRadius | `float`     |         |

> **Returns** - `IEnumerable\<Vector>`

[0]: ../../../Heirloom.Core.md
[1]: ../GeometryTools.md
[2]: ../../Heirloom/Vector.md
