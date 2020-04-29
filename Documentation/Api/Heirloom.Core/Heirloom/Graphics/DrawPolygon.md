# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Graphics.DrawPolygon

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Graphics][1]  

### DrawPolygon(in Vector, int, float)

Draws a regular polygon to the current surface.

```cs
public void DrawPolygon(in Vector position, int sides, float radius)
```

### DrawPolygon(Polygon)

Draws a simple polygon to the current surface.

```cs
public void DrawPolygon(Polygon polygon)
```

### DrawPolygon(Polygon, in Matrix)

Draws a simple polygon to the current surface.

```cs
public void DrawPolygon(Polygon polygon, in Matrix transform)
```

### DrawPolygon(IEnumerable<Vector>)

Draws a simple polygon to the current surface.

```cs
public void DrawPolygon(IEnumerable<Vector> polygon)
```

### DrawPolygon(IEnumerable<Vector>, in Matrix)

Draws a simple polygon to the current surface.

```cs
public void DrawPolygon(IEnumerable<Vector> polygon, in Matrix transform)
```

[0]: ../../../Heirloom.Core.md
[1]: ../Graphics.md
