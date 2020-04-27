# NineSlice

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

A special stretchable rectangle of an image.

```cs
public class NineSlice
```

--------------------------------------------------------------------------------

**Fields**: [Image][1], [Center][2]

--------------------------------------------------------------------------------

## Constructors

### NineSlice(Image, IntRectangle)

Constructs a new nine slice.

```cs
public NineSlice(Image frame, IntRectangle center)
```

## Fields

| Name        | Summary                                                                                 |
|-------------|-----------------------------------------------------------------------------------------|
| [Image][1]  | The image used by this nine slice.                                                      |
| [Center][2] | The center rectangle of the nine slice. This implicitly defines all other slice bounds. |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.NineSlice.Image.md
[2]: Heirloom.NineSlice.Center.md