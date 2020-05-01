# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Circle.Raycast (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [Circle][1]

### Raycast(in Ray)

Peforms a raycast onto this circle, returning true upon intersection.

```cs
public bool Raycast(in Ray ray)
```

| Name | Type     | Summary |
|------|----------|---------|
| ray  | [Ray][2] |         |

> **Returns** - `bool`

### Raycast(in Ray, out RayContact)

Peforms a raycast onto this circle, returning true upon intersection.

```cs
public bool Raycast(in Ray ray, out RayContact contact)
```

| Name    | Type            | Summary                       |
|---------|-----------------|-------------------------------|
| ray     | [Ray][2]        | Some ray.                     |
| contact | [RayContact][3] | Ray intersection information. |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../Circle.md
[2]: ../Ray.md
[3]: ../RayContact.md
