# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IntRectangle.Offset (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [IntRectangle][1]

### Offset(int, int)

Translates this rectangle.

```cs
public void Offset(int x, int y)
```

| Name | Type  | Summary |
|------|-------|---------|
| x    | `int` |         |
| y    | `int` |         |

> **Returns** - `void`

### Offset(IntVector)

Translates this rectangle.

```cs
public void Offset(IntVector offset)
```

| Name   | Type           | Summary |
|--------|----------------|---------|
| offset | [IntVector][2] |         |

> **Returns** - `void`

### Offset(IntRectangle, int, int)

Copies and translates the given rectangle.

```cs
public static IntRectangle Offset(IntRectangle rect, int x, int y)
```

| Name | Type              | Summary |
|------|-------------------|---------|
| rect | [IntRectangle][1] |         |
| x    | `int`             |         |
| y    | `int`             |         |

> **Returns** - [IntRectangle][1]

### Offset(IntRectangle, IntVector)

Copies and translates the given rectangle.

```cs
public static IntRectangle Offset(IntRectangle rect, IntVector offset)
```

| Name   | Type              | Summary |
|--------|-------------------|---------|
| rect   | [IntRectangle][1] |         |
| offset | [IntVector][2]    |         |

> **Returns** - [IntRectangle][1]

[0]: ../../../Heirloom.Core.md
[1]: ../IntRectangle.md
[2]: ../IntVector.md
