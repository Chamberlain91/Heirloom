# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Rasterizer.Line (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Rasterizer][1]

### Line(IntVector, IntVector)

Rasterize along a line.

```cs
public static IEnumerable<IntVector> Line(IntVector p0, IntVector p1)
```

| Name | Type           | Summary         |
|------|----------------|-----------------|
| p0   | [IntVector][2] | Starting point. |
| p1   | [IntVector][2] | Ending point.   |

> **Returns** - `IEnumerable<IntVector>`

### Line(IntVector, IntVector, byte)

Rasterize along a line.

```cs
public static IEnumerable<IntVector> Line(IntVector p0, IntVector p1, byte pattern)
```

| Name    | Type           | Summary                                    |
|---------|----------------|--------------------------------------------|
| p0      | [IntVector][2] | Starting point.                            |
| p1      | [IntVector][2] | Ending point.                              |
| pattern | ` byte`        | Sequence of bits to mask drawing the line. |

> **Returns** - `IEnumerable<IntVector>`

### Line(IntVector, IntVector, ushort)

Rasterize along a line.

```cs
public static IEnumerable<IntVector> Line(IntVector p0, IntVector p1, ushort pattern)
```

| Name    | Type           | Summary                                    |
|---------|----------------|--------------------------------------------|
| p0      | [IntVector][2] | Starting point.                            |
| p1      | [IntVector][2] | Ending point.                              |
| pattern | `ushort`       | Sequence of bits to mask drawing the line. |

> **Returns** - `IEnumerable<IntVector>`

### Line(IntVector, IntVector, uint)

Rasterize along a line.

```cs
public static IEnumerable<IntVector> Line(IntVector p0, IntVector p1, uint pattern)
```

| Name    | Type           | Summary                                    |
|---------|----------------|--------------------------------------------|
| p0      | [IntVector][2] | Starting point.                            |
| p1      | [IntVector][2] | Ending point.                              |
| pattern | `uint`         | Sequence of bits to mask drawing the line. |

> **Returns** - `IEnumerable<IntVector>`

[0]: ../../../Heirloom.Core.md
[1]: ../Rasterizer.md
[2]: ../IntVector.md
