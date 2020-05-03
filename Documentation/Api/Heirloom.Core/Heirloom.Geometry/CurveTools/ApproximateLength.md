# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## CurveTools.ApproximateLength (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [CurveTools][1]

### ApproximateLength(Vector, Vector, Vector, int)

Computes the approximate arc length of the quadratic curve using line segments.

```cs
public static float ApproximateLength(Vector a, Vector b, Vector c, int resolution = 4)
```

| Name       | Type        | Summary                                               |
|------------|-------------|-------------------------------------------------------|
| a          | [Vector][2] | The curve starting point.                             |
| b          | [Vector][2] | The curve middle (handle).                            |
| c          | [Vector][2] | The curve ending point.                               |
| resolution | `int`       | The number of segments to approximate the curve with. |

> **Returns** - `float`

### ApproximateLength(Vector, Vector, Vector, Vector, int)

Computes the approximate arc length of the cubic curve using line segments.

```cs
public static float ApproximateLength(Vector a, Vector b, Vector c, Vector d, int resolution = 5)
```

| Name       | Type        | Summary                                               |
|------------|-------------|-------------------------------------------------------|
| a          | [Vector][2] | The curve's starting point.                           |
| b          | [Vector][2] | The curve's first handle.                             |
| c          | [Vector][2] | The curve's second handle.                            |
| d          | [Vector][2] | The curve's ending point.                             |
| resolution | `int`       | The number of segments to approximate the curve with. |

> **Returns** - `float`

### ApproximateLength(Func<float, Vector>, int)

Helper function to compute the approximate length of a curve using discrete segments.

```cs
public static float ApproximateLength(Func<float, Vector> getPoint, int resolution = 5)
```

| Name       | Type                   | Summary                                        |
|------------|------------------------|------------------------------------------------|
| getPoint   | `Func\<float, Vector>` | A function to get a interpolated point.        |
| resolution | `int`                  | The number of discretes steps along the curve. |

> **Returns** - `float`

[0]: ../../../Heirloom.Core.md
[1]: ../CurveTools.md
[2]: ../../Heirloom/Vector.md
