# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IntRectangle.Include (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [IntRectangle][1]

### Include(IntVector)

Mutates this rectangle to accommodate the given point.

```cs
public void Include(IntVector point)
```

| Name  | Type           | Summary                |
|-------|----------------|------------------------|
| point | [IntVector][2] | Some point to include. |

> **Returns** - `void`

Useful for computing a bounding rectangle.

### Include(in IntRectangle)

Mutates this rectangle to accommodate the given rectangle.

```cs
public void Include(in IntRectangle rect)
```

| Name | Type              | Summary                    |
|------|-------------------|----------------------------|
| rect | [IntRectangle][1] | Some rectangle to include. |

> **Returns** - `void`

Useful for computing a bounding rectangle.

[0]: ../../../Heirloom.Core.md
[1]: ../IntRectangle.md
[2]: ../IntVector.md
