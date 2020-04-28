# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## PolygonTools.Raycast

> **Namespace**: [Heirloom][0]  
> **Type**: [PolygonTools][1]  

### Raycast(IReadOnlyList<Vector>, in Ray)

Checks if a ray intersects this polygon.

```cs
public static bool Raycast(IReadOnlyList<Vector> polygon, in Ray ray)
```

### Raycast(IReadOnlyList<Vector>, in Vector, in Vector)

Checks if a ray intersects this polygon.

```cs
public static bool Raycast(IReadOnlyList<Vector> polygon, in Vector origin, in Vector direction)
```

### Raycast(IReadOnlyList<Vector>, in Ray, out RayContact)

Checks if a ray intersects this polygon and outputs information on the contact point.

```cs
public static bool Raycast(IReadOnlyList<Vector> polygon, in Ray ray, out RayContact contact)
```

### Raycast(IReadOnlyList<Vector>, in Vector, in Vector, out RayContact)

Checks if a ray intersects this polygon and outputs information on the contact point.

```cs
public static bool Raycast(IReadOnlyList<Vector> polygon, in Vector origin, in Vector direction, out RayContact contact)
```

[0]: ../../../Heirloom.Core.md
[1]: ../PolygonTools.md
