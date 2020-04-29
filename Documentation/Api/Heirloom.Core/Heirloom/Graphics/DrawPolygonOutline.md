# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graphics.DrawPolygonOutline (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Graphics][1]

### DrawPolygonOutline(in Vector, int, float, float)

Draws the outline of a regular polygon to the current surface.

```cs
public void DrawPolygonOutline(in Vector position, int sides, float radius, float width = 1)
```

| Name     | Type        | Summary                                     |
|----------|-------------|---------------------------------------------|
| position | [Vector][2] |                                             |
| sides    | `int`       | The number of sides in the regular polygon. |
| radius   | `float`     | The radius of the regular polygon.          |
| width    | `float`     | Width of the outline in pixels.             |

> **Returns** - `void`

### DrawPolygonOutline(Polygon, float)

Draws the outline of a simple polygon to the current surface.

```cs
public void DrawPolygonOutline(Polygon polygon, float width = 1)
```

| Name    | Type         | Summary                         |
|---------|--------------|---------------------------------|
| polygon | [Polygon][3] | Some polygon.                   |
| width   | `float`      | Width of the outline in pixels. |

> **Returns** - `void`

### DrawPolygonOutline(Polygon, in Matrix, float)

Draws the outline of a simple polygon to the current surface.

```cs
public void DrawPolygonOutline(Polygon polygon, in Matrix transform, float width = 1)
```

| Name      | Type         | Summary                         |
|-----------|--------------|---------------------------------|
| polygon   | [Polygon][3] | Some polygon.                   |
| transform | [Matrix][4]  | Some transform.                 |
| width     | `float`      | Width of the outline in pixels. |

> **Returns** - `void`

### DrawPolygonOutline(IEnumerable<Vector>, float)

Draws the outline of a simple polygon to the current surface.

```cs
public void DrawPolygonOutline(IEnumerable<Vector> polygon, float width = 1)
```

| Name    | Type                   | Summary                         |
|---------|------------------------|---------------------------------|
| polygon | `IEnumerable\<Vector>` | Some polygon.                   |
| width   | `float`                | Width of the outline in pixels. |

> **Returns** - `void`

### DrawPolygonOutline(IEnumerable<Vector>, in Matrix, float)

Draws the outline of a simple polygon to the current surface.

```cs
public void DrawPolygonOutline(IEnumerable<Vector> polygon, in Matrix transform, float width = 1)
```

| Name      | Type                   | Summary                         |
|-----------|------------------------|---------------------------------|
| polygon   | `IEnumerable\<Vector>` | Some polygon.                   |
| transform | [Matrix][4]            | Some transform.                 |
| width     | `float`                | Width of the outline in pixels. |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Graphics.md
[2]: ../Vector.md
[3]: ../Polygon.md
[4]: ../Matrix.md
