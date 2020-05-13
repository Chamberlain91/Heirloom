# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## PolygonTools.ComputeMetrics (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [PolygonTools][1]

### ComputeMetrics(IReadOnlyList<Vector>, out float, out Vector, out Vector)

Computes general metrics about the specified polygon. Outputs the `area` , `center` and `centroid` .

```cs
public static void ComputeMetrics(IReadOnlyList<Vector> polygon, out float area, out Vector center, out Vector centroid)
```

| Name     | Type                    | Summary                                     |
|----------|-------------------------|---------------------------------------------|
| polygon  | `IReadOnlyList<Vector>` | Some polygon.                               |
| area     | `float`                 | The area occupied by the polygon.           |
| center   | [Vector][2]             | The center of the polygon by average.       |
| centroid | [Vector][2]             | The center of the polygon weighted by area. |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../PolygonTools.md
[2]: ../../Heirloom/Vector.md
