# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GraphicsContext.DrawPolygon (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [GraphicsContext][1]

### DrawPolygon(in Vector, int, float)

Draws a regular polygon to the current surface.

```cs
public void DrawPolygon(in Vector position, int sides, float radius)
```

| Name     | Type        | Summary                                     |
|----------|-------------|---------------------------------------------|
| position | [Vector][2] |                                             |
| sides    | `int`       | The number of sides in the regular polygon. |
| radius   | `float`     | The radius of the regular polygon.          |

> **Returns** - `void`

### DrawPolygon(Polygon)

Draws a simple polygon to the current surface.

```cs
public void DrawPolygon(Polygon polygon)
```

| Name    | Type         | Summary       |
|---------|--------------|---------------|
| polygon | [Polygon][3] | Some polygon. |

> **Returns** - `void`

### DrawPolygon(Polygon, in Matrix)

Draws a simple polygon to the current surface.

```cs
public void DrawPolygon(Polygon polygon, in Matrix transform)
```

| Name      | Type         | Summary         |
|-----------|--------------|-----------------|
| polygon   | [Polygon][3] | Some polygon.   |
| transform | [Matrix][4]  | Some transform. |

> **Returns** - `void`

### DrawPolygon(IEnumerable<Vector>)

Draws a simple polygon to the current surface.

```cs
public void DrawPolygon(IEnumerable<Vector> polygon)
```

| Name    | Type                  | Summary       |
|---------|-----------------------|---------------|
| polygon | `IEnumerable<Vector>` | Some polygon. |

> **Returns** - `void`

### DrawPolygon(IEnumerable<Vector>, in Matrix)

Draws a simple polygon to the current surface.

```cs
public void DrawPolygon(IEnumerable<Vector> polygon, in Matrix transform)
```

| Name      | Type                  | Summary         |
|-----------|-----------------------|-----------------|
| polygon   | `IEnumerable<Vector>` | Some polygon.   |
| transform | [Matrix][4]           | Some transform. |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../GraphicsContext.md
[2]: ../Vector.md
[3]: ../../Heirloom.Geometry/Polygon.md
[4]: ../Matrix.md
