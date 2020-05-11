# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Curve.Add (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [Curve][1]

### Add(Vector, Vector, Vector, CurveType)

Adds a segment to the end of the curve.

```cs
public void Add(Vector controlPoint, Vector inHandle, Vector outHandle, CurveType type = Cubic)
```

| Name         | Type           | Summary                                                     |
|--------------|----------------|-------------------------------------------------------------|
| controlPoint | [Vector][2]    | The control point.                                          |
| inHandle     | [Vector][2]    | The first handle, relative to this newly added point.       |
| outHandle    | [Vector][2]    | The second handle, relative to the next point in the curve. |
| type         | [CurveType][3] |                                                             |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Curve.md
[2]: ../../Heirloom/Vector.md
[3]: ../CurveType.md
