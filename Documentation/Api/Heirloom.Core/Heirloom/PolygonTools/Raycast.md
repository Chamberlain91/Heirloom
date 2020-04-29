# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## PolygonTools.Raycast (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [PolygonTools][1]

### Raycast(IReadOnlyList<Vector>, in Ray)

Checks if a ray intersects this polygon.

```cs
public static bool Raycast(IReadOnlyList<Vector> polygon, in Ray ray)
```

| Name    | Type                     | Summary |
|---------|--------------------------|---------|
| polygon | `IReadOnlyList\<Vector>` |         |
| ray     | [Ray][2]                 |         |

> **Returns** - `bool`

### Raycast(IReadOnlyList<Vector>, in Vector, in Vector)

Checks if a ray intersects this polygon.

```cs
public static bool Raycast(IReadOnlyList<Vector> polygon, in Vector origin, in Vector direction)
```

| Name      | Type                     | Summary |
|-----------|--------------------------|---------|
| polygon   | `IReadOnlyList\<Vector>` |         |
| origin    | [Vector][3]              |         |
| direction | [Vector][3]              |         |

> **Returns** - `bool`

### Raycast(IReadOnlyList<Vector>, in Ray, out RayContact)

Checks if a ray intersects this polygon and outputs information on the contact point.

```cs
public static bool Raycast(IReadOnlyList<Vector> polygon, in Ray ray, out RayContact contact)
```

| Name    | Type                     | Summary |
|---------|--------------------------|---------|
| polygon | `IReadOnlyList\<Vector>` |         |
| ray     | [Ray][2]                 |         |
| contact | [RayContact][4]          |         |

> **Returns** - `bool`

### Raycast(IReadOnlyList<Vector>, in Vector, in Vector, out RayContact)

Checks if a ray intersects this polygon and outputs information on the contact point.

```cs
public static bool Raycast(IReadOnlyList<Vector> polygon, in Vector origin, in Vector direction, out RayContact contact)
```

| Name      | Type                     | Summary |
|-----------|--------------------------|---------|
| polygon   | `IReadOnlyList\<Vector>` |         |
| origin    | [Vector][3]              |         |
| direction | [Vector][3]              |         |
| contact   | [RayContact][4]          |         |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../PolygonTools.md
[2]: ../Ray.md
[3]: ../Vector.md
[4]: ../RayContact.md
