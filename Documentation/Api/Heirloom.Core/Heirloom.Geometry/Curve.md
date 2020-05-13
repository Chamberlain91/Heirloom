# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Curve (Class)

> **Namespace**: [Heirloom.Geometry][0]

An implementation of a multi-point bezier curve using multiple 'segments' of simple curves.

```cs
public sealed class Curve
```

### Properties

[Count][1]

### Methods

[Add][2], [GetCurveType][3], [GetInHandle][4], [GetOutHandle][5], [GetPoint][6], [Insert][7], [Interpolate][8], [InterpolateDerivative][9], [RemoveAt][10], [SetCurveType][11], [SetInHandle][12], [SetOutHandle][13], [SetPoint][14]

## Properties

#### Instance

| Name       | Type  | Summary                               |
|------------|-------|---------------------------------------|
| [Count][1] | `int` | The number of segments in this curve. |

## Methods

#### Instance

| Name                            | Return Type     | Summary                                                                |
|---------------------------------|-----------------|------------------------------------------------------------------------|
| [Add(Vector, Vector, Ve...][2]  | `void`          | Adds a segment to the end of the curve.                                |
| [GetCurveType(int)][3]          | [CurveType][15] | Gets the type of curve of the segment following the point at the sp... |
| [GetInHandle(int)][4]           | [Vector][16]    | Gets the incoming handle point of the curve at the specified index.    |
| [GetOutHandle(int)][5]          | [Vector][16]    | Gets the outgoing handle point of the curve at the specified index.    |
| [GetPoint(int)][6]              | [Vector][16]    | Gets the point of the curve at the specified index.                    |
| [Insert(int, Vector, Ve...][7]  | `void`          | Inserts a segment into the curve.                                      |
| [Interpolate(float)][8]         | [Vector][16]    | Computes a point interpolated across the curve.                        |
| [InterpolateDerivative(...][9]  | [Vector][16]    | Computes the derivative of a point interpolated across the curve.      |
| [RemoveAt(int)][10]             | `void`          | Removes a segment from the curve.                                      |
| [SetCurveType(int, Curv...][11] | `void`          | Sets the type of curve of the segment following the point at the sp... |
| [SetInHandle(int, Vector)][12]  | `void`          | Sets the incoming handle point of the curve at the specified index.    |
| [SetOutHandle(int, Vector)][13] | `void`          | Sets the outgoing handle point of the curve at the specified index.    |
| [SetPoint(int, Vector)][14]     | `void`          | Sets the point of the curve at the specified index.                    |

[0]: ../../Heirloom.Core.md
[1]: Curve/Count.md
[2]: Curve/Add.md
[3]: Curve/GetCurveType.md
[4]: Curve/GetInHandle.md
[5]: Curve/GetOutHandle.md
[6]: Curve/GetPoint.md
[7]: Curve/Insert.md
[8]: Curve/Interpolate.md
[9]: Curve/InterpolateDerivative.md
[10]: Curve/RemoveAt.md
[11]: Curve/SetCurveType.md
[12]: Curve/SetInHandle.md
[13]: Curve/SetOutHandle.md
[14]: Curve/SetPoint.md
[15]: CurveType.md
[16]: ../Heirloom/Vector.md
