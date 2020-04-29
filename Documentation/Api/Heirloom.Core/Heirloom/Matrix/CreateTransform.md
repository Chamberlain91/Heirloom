# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Matrix.CreateTransform

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Matrix][1]  

### CreateTransform(in float, in float, in float, in float, in float)

Creates a transform matrix with postion, rotation and scale.

```cs
public static Matrix CreateTransform(in float tx, in float ty, in float angle, in float sx, in float sy)
```

### CreateTransform(in Vector, float, in Vector)

Creates a transform matrix with postion, rotation and scale.

```cs
public static Matrix CreateTransform(in Vector position, float angle, in Vector scale)
```

### CreateTransform(in Vector, float, in float)

Creates a transform matrix with postion, rotation and scale.

```cs
public static Matrix CreateTransform(in Vector position, float angle, in float scale)
```

[0]: ../../../Heirloom.Core.md
[1]: ../Matrix.md
