# Graphics.DrawPolygonOutline

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Graphics][1]  

--------------------------------------------------------------------------------

### DrawPolygonOutline(in Vector, int, float, float)

Draws the outline of a regular polygon to the current surface.

```cs
public void DrawPolygonOutline(in Vector position, int sides, float radius, float width = 1)
```

### DrawPolygonOutline(Polygon, float)

Draws the outline of a simple polygon to the current surface.

```cs
public void DrawPolygonOutline(Polygon polygon, float width = 1)
```

### DrawPolygonOutline(Polygon, in Matrix, float)

Draws the outline of a simple polygon to the current surface.

```cs
public void DrawPolygonOutline(Polygon polygon, in Matrix transform, float width = 1)
```

### DrawPolygonOutline(IEnumerable<Vector>, float)

Draws the outline of a simple polygon to the current surface.

```cs
public void DrawPolygonOutline(IEnumerable<Vector> polygon, float width = 1)
```

### DrawPolygonOutline(IEnumerable<Vector>, in Matrix, float)

Draws the outline of a simple polygon to the current surface.

```cs
public void DrawPolygonOutline(IEnumerable<Vector> polygon, in Matrix transform, float width = 1)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.Graphics.md
