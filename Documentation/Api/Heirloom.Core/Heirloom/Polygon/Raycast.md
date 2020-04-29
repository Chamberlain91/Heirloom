# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Polygon.Raycast (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Polygon][1]

### Raycast(in Ray)

Checks if a ray intersects this polygon.

```cs
public bool Raycast(in Ray ray)
```

| Name | Type     | Summary |
|------|----------|---------|
| ray  | [Ray][2] |         |

> **Returns** - `bool`

### Raycast(in Ray, out RayContact)

Checks if a ray intersects this polygon and outputs information on the contact point.

```cs
public bool Raycast(in Ray ray, out RayContact hit)
```

| Name | Type            | Summary |
|------|-----------------|---------|
| ray  | [Ray][2]        |         |
| hit  | [RayContact][3] |         |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../Polygon.md
[2]: ../Ray.md
[3]: ../RayContact.md
