# Polygon.Raycast

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Polygon][1]  

--------------------------------------------------------------------------------

### Raycast(in Ray)

Checks if a ray intersects this polygon.

```cs
public bool Raycast(in Ray ray)
```

### Raycast(in Ray, out RayContact)

Checks if a ray intersects this polygon and outputs information on the contact point.

```cs
public bool Raycast(in Ray ray, out RayContact hit)
```

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Polygon.md