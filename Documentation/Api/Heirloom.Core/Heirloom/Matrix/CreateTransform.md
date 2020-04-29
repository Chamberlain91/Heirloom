# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Matrix.CreateTransform (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Matrix][1]

### CreateTransform(in float, in float, in float, in float, in float)

Creates a transform matrix with postion, rotation and scale.

```cs
public static Matrix CreateTransform(in float tx, in float ty, in float angle, in float sx, in float sy)
```

| Name  | Type    | Summary |
|-------|---------|---------|
| tx    | `float` |         |
| ty    | `float` |         |
| angle | `float` |         |
| sx    | `float` |         |
| sy    | `float` |         |

> **Returns** - [Matrix][1]

### CreateTransform(in Vector, float, in Vector)

Creates a transform matrix with postion, rotation and scale.

```cs
public static Matrix CreateTransform(in Vector position, float angle, in Vector scale)
```

| Name     | Type        | Summary |
|----------|-------------|---------|
| position | [Vector][2] |         |
| angle    | `float`     |         |
| scale    | [Vector][2] |         |

> **Returns** - [Matrix][1]

### CreateTransform(in Vector, float, in float)

Creates a transform matrix with postion, rotation and scale.

```cs
public static Matrix CreateTransform(in Vector position, float angle, in float scale)
```

| Name     | Type        | Summary |
|----------|-------------|---------|
| position | [Vector][2] |         |
| angle    | `float`     |         |
| scale    | `float`     |         |

> **Returns** - [Matrix][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Matrix.md
[2]: ../Vector.md
