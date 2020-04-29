# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Rectangle.Include (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Rectangle][1]

### Include(Vector)

Mutates this rectangle to accommodate the given point.

```cs
public void Include(Vector point)
```

| Name  | Type        | Summary                |
|-------|-------------|------------------------|
| point | [Vector][2] | Some point to include. |

> **Returns** - `void`

Useful for computing a bounding rectangle.

### Include(in Rectangle)

Mutates this rectangle to accommodate the given rectangle.

```cs
public void Include(in Rectangle rect)
```

| Name | Type           | Summary                    |
|------|----------------|----------------------------|
| rect | [Rectangle][1] | Some rectangle to include. |

> **Returns** - `void`

Useful for computing a bounding rectangle.

[0]: ../../../Heirloom.Core.md
[1]: ../Rectangle.md
[2]: ../Vector.md
