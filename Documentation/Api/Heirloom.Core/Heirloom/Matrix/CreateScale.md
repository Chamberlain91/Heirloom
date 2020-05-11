# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Matrix.CreateScale (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Matrix][1]

### CreateScale(float, float)

Constructs a new scaling matrix.

```cs
public static Matrix CreateScale(float sx, float sy)
```

| Name | Type    | Summary |
|------|---------|---------|
| sx   | `float` |         |
| sy   | `float` |         |

> **Returns** - [Matrix][1]

### CreateScale(in Size)

Constructs a new scaling matrix.

```cs
public static Matrix CreateScale(in Size scale)
```

| Name  | Type      | Summary |
|-------|-----------|---------|
| scale | [Size][2] |         |

> **Returns** - [Matrix][1]

### CreateScale(in Vector)

Constructs a new scaling matrix.

```cs
public static Matrix CreateScale(in Vector scale)
```

| Name  | Type        | Summary |
|-------|-------------|---------|
| scale | [Vector][3] |         |

> **Returns** - [Matrix][1]

### CreateScale(float)

Constructs a new uniform scaling matrix.

```cs
public static Matrix CreateScale(float scale)
```

| Name  | Type    | Summary |
|-------|---------|---------|
| scale | `float` |         |

> **Returns** - [Matrix][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Matrix.md
[2]: ../Size.md
[3]: ../Vector.md
