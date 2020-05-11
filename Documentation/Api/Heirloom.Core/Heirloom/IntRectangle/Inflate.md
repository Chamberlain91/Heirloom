# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IntRectangle.Inflate (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [IntRectangle][1]

### Inflate(int)

Expands (or shrinks) the rectangle by a factor on both axis.

```cs
public void Inflate(int factor)
```

| Name   | Type  | Summary |
|--------|-------|---------|
| factor | `int` |         |

> **Returns** - `void`

### Inflate(int, int)

Expands (or shrinks) the rectangle by a factor on each axis.

```cs
public void Inflate(int xFactor, int yFactor)
```

| Name    | Type  | Summary |
|---------|-------|---------|
| xFactor | `int` |         |
| yFactor | `int` |         |

> **Returns** - `void`

### Inflate(IntRectangle, int)

Expands (or shrinks) the input rectangle by a factor on both axis.

```cs
public static IntRectangle Inflate(IntRectangle rect, int factor)
```

| Name   | Type              | Summary |
|--------|-------------------|---------|
| rect   | [IntRectangle][1] |         |
| factor | `int`             |         |

> **Returns** - [IntRectangle][1]

### Inflate(IntRectangle, int, int)

Expands (or shrinks) the input rectangle by a factor on each axis.

```cs
public static IntRectangle Inflate(IntRectangle rect, int xFactor, int yFactor)
```

| Name    | Type              | Summary |
|---------|-------------------|---------|
| rect    | [IntRectangle][1] |         |
| xFactor | `int`             |         |
| yFactor | `int`             |         |

> **Returns** - [IntRectangle][1]

[0]: ../../../Heirloom.Core.md
[1]: ../IntRectangle.md
