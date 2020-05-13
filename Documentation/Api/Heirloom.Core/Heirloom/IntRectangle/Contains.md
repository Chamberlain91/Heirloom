# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IntRectangle.Contains (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [IntRectangle][1]

### Contains(in Vector)

Determines if this rectangle contains the given point?

```cs
public bool Contains(in Vector point)
```

| Name  | Type        | Summary |
|-------|-------------|---------|
| point | [Vector][2] |         |

> **Returns** - `bool`

### Contains(in IntVector)

Determines if this rectangle contains the given point?

```cs
public bool Contains(in IntVector point)
```

| Name  | Type           | Summary |
|-------|----------------|---------|
| point | [IntVector][3] |         |

> **Returns** - `bool`

### Contains(in IntRectangle)

Determines if this rectangle contains another rectangle?

```cs
public bool Contains(in IntRectangle other)
```

| Name  | Type              | Summary |
|-------|-------------------|---------|
| other | [IntRectangle][1] |         |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../IntRectangle.md
[2]: ../Vector.md
[3]: ../IntVector.md
