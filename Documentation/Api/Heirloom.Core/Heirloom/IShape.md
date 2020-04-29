# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IShape (Interface)

> **Namespace**: [Heirloom][0]

Represents the general interface of a shape and common operators each shape should support.

```cs
public interface IShape
```

### Properties

[Area][1], [Bounds][2]

### Methods

[Contains][3], [GetClosestPoint][4], [Overlaps][5], [Project][6], [Raycast][7]

## Properties

#### Instance

| Name        | Type           | Summary                                    |
|-------------|----------------|--------------------------------------------|
| [Area][1]   | `float`        | Gets the area of this shape.               |
| [Bounds][2] | [Rectangle][8] | Gets the bounding rectangle of this shape. |

## Methods

#### Instance

| Name                           | Return Type | Summary                                                     |
|--------------------------------|-------------|-------------------------------------------------------------|
| [Contains(in Vector)][3]       | `bool`      | Determines if this shape contains the specified point.      |
| [GetClosestPoint(in Vec...][4] | [Vector][9] | Gets the nearest point on the shape to the specified point. |
| [Overlaps(IShape)][5]          | `bool`      | Determines if this shape overlaps the specified shape.      |
| [Project(in Vector)][6]        | [Range][10] | Project this shape onto the specified axis.                 |
| [Raycast(in Ray, out Ra...][7] | `bool`      | Performs a raycast against this shape.                      |
| [Raycast(in Ray)][7]           | `bool`      | Performs a raycast against this shape.                      |

[0]: ../../Heirloom.Core.md
[1]: IShape/Area.md
[2]: IShape/Bounds.md
[3]: IShape/Contains.md
[4]: IShape/GetClosestPoint.md
[5]: IShape/Overlaps.md
[6]: IShape/Project.md
[7]: IShape/Raycast.md
[8]: Rectangle.md
[9]: Vector.md
[10]: Range.md
