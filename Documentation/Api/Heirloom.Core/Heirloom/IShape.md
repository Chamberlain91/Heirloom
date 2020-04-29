# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## IShape Interface

> **Namespace**: [Heirloom][0]  

Represents the general interface of a shape and common operators each shape should support.

```cs
public interface IShape
```

#### Properties

[Bounds][1], [Area][2]

#### Methods

[GetClosestPoint][3], [Contains][4], [Overlaps][5], [Raycast][6], [Project][7]

## Properties

| Name        | Summary                                    |
|-------------|--------------------------------------------|
| [Bounds][1] | Gets the bounding rectangle of this shape. |
| [Area][2]   | Gets the area of this shape.               |

## Methods

| Name                 | Summary                                                     |
|----------------------|-------------------------------------------------------------|
| [GetClosestPoint][3] | Gets the nearest point on the shape to the specified point. |
| [Contains][4]        | Determines if this shape contains the specified point.      |
| [Overlaps][5]        | Determines if this shape overlaps the specified shape.      |
| [Raycast][6]         | Performs a raycast against this shape.                      |
| [Raycast][6]         | Performs a raycast against this shape.                      |
| [Project][7]         | Project this shape onto the specified axis.                 |

[0]: ../../Heirloom.Core.md
[1]: IShape/Bounds.md
[2]: IShape/Area.md
[3]: IShape/GetClosestPoint.md
[4]: IShape/Contains.md
[5]: IShape/Overlaps.md
[6]: IShape/Raycast.md
[7]: IShape/Project.md
