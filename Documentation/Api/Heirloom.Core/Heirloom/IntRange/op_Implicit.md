# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IntRange.Implicit (Operator)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [IntRange][1]

### Implicit(IntRange)

```cs
public static Range Implicit(IntRange vec)
```

| Name | Type          | Summary |
|------|---------------|---------|
| vec  | [IntRange][1] |         |

> **Returns** - [Range][2]

### Implicit(IntRange)

```cs
public static ValueTuple<int, int> Implicit(IntRange range)
```

| Name  | Type          | Summary |
|-------|---------------|---------|
| range | [IntRange][1] |         |

> **Returns** - `ValueTuple<int, int>`

### Implicit(ValueTuple<int, int>)

```cs
public static IntRange Implicit(ValueTuple<int, int> vec)
```

| Name | Type                   | Summary |
|------|------------------------|---------|
| vec  | `ValueTuple<int, int>` |         |

> **Returns** - [IntRange][1]

[0]: ../../../Heirloom.Core.md
[1]: ../IntRange.md
[2]: ../Range.md
