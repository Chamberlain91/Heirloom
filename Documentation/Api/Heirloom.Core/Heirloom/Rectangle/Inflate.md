# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Rectangle.Inflate (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Rectangle][1]

### Inflate(float)

Expands (or shrinks) the rectangle by a factor on both axis.

```cs
public void Inflate(float factor)
```

| Name   | Type    | Summary |
|--------|---------|---------|
| factor | `float` |         |

> **Returns** - `void`

### Inflate(float, float)

Expands (or shrinks) the rectangle by a factor on each axis.

```cs
public void Inflate(float xFactor, float yFactor)
```

| Name    | Type    | Summary |
|---------|---------|---------|
| xFactor | `float` |         |
| yFactor | `float` |         |

> **Returns** - `void`

### Inflate(Rectangle, float)

Expands (or shrinks) the input rectangle by a factor on both axis.

```cs
public static Rectangle Inflate(Rectangle rect, float factor)
```

| Name   | Type           | Summary |
|--------|----------------|---------|
| rect   | [Rectangle][1] |         |
| factor | `float`        |         |

> **Returns** - [Rectangle][1]

### Inflate(Rectangle, float, float)

Expands (or shrinks) the input rectangle by a factor on each axis.

```cs
public static Rectangle Inflate(Rectangle rect, float xFactor, float yFactor)
```

| Name    | Type           | Summary |
|---------|----------------|---------|
| rect    | [Rectangle][1] |         |
| xFactor | `float`        |         |
| yFactor | `float`        |         |

> **Returns** - [Rectangle][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Rectangle.md
