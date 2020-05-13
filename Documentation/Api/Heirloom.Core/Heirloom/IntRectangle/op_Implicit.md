# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IntRectangle.Implicit (Operator)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [IntRectangle][1]

### Implicit(IntRectangle)

```cs
public static Rectangle Implicit(IntRectangle rect)
```

| Name | Type              | Summary |
|------|-------------------|---------|
| rect | [IntRectangle][1] |         |

> **Returns** - [Rectangle][2]

### Implicit(ValueTuple<int, int, int, int>)

```cs
public static IntRectangle Implicit(ValueTuple<int, int, int, int> rect)
```

| Name | Type                             | Summary |
|------|----------------------------------|---------|
| rect | `ValueTuple<int, int, int, int>` |         |

> **Returns** - [IntRectangle][1]

### Implicit(ValueTuple<IntVector, IntSize>)

```cs
public static IntRectangle Implicit(ValueTuple<IntVector, IntSize> rect)
```

| Name | Type                             | Summary |
|------|----------------------------------|---------|
| rect | `ValueTuple<IntVector, IntSize>` |         |

> **Returns** - [IntRectangle][1]

[0]: ../../../Heirloom.Core.md
[1]: ../IntRectangle.md
[2]: ../Rectangle.md
