# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IShape.Raycast (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [IShape][1]

### Raycast(in Ray, out RayContact)

Performs a raycast against this shape.

```cs
public abstract bool Raycast(in Ray ray, out RayContact contact)
```

| Name    | Type            | Summary |
|---------|-----------------|---------|
| ray     | [Ray][2]        |         |
| contact | [RayContact][3] |         |

> **Returns** - `bool`

### Raycast(in Ray)

Performs a raycast against this shape.

```cs
public abstract bool Raycast(in Ray ray)
```

| Name | Type     | Summary |
|------|----------|---------|
| ray  | [Ray][2] |         |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../IShape.md
[2]: ../Ray.md
[3]: ../RayContact.md
