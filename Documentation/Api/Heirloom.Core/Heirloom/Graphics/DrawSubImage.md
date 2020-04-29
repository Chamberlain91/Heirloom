# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graphics.DrawSubImage (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Graphics][1]

### DrawSubImage(ImageSource, in IntRectangle, in Rectangle)

Draws a sub-region of an image stretched to fill a rectangular region to the current surface ignoring the image origin offset.

```cs
public void DrawSubImage(ImageSource image, in IntRectangle region, in Rectangle rectangle)
```

| Name      | Type              | Summary                        |
|-----------|-------------------|--------------------------------|
| image     | [ImageSource][2]  | Some image.                    |
| region    | [IntRectangle][3] | Some subregion of the image.   |
| rectangle | [Rectangle][4]    | The bounds of the drawn image. |

> **Returns** - `void`

### DrawSubImage(ImageSource, in IntRectangle, in Vector)

Draws a sub-region of an image to the current surface ignoring the image origin offset.

```cs
public void DrawSubImage(ImageSource image, in IntRectangle region, in Vector position)
```

| Name     | Type              | Summary                      |
|----------|-------------------|------------------------------|
| image    | [ImageSource][2]  | Some image.                  |
| region   | [IntRectangle][3] | Some subregion of the image. |
| position | [Vector][5]       | The position of the image.   |

> **Returns** - `void`

### DrawSubImage(ImageSource, in IntRectangle, in Matrix)

Draws a sub-region of an image to the current surface ignoring the image origin offset.

```cs
public void DrawSubImage(ImageSource image, in IntRectangle region, in Matrix transform)
```

| Name      | Type              | Summary                      |
|-----------|-------------------|------------------------------|
| image     | [ImageSource][2]  | Some image.                  |
| region    | [IntRectangle][3] | Some subregion of the image. |
| transform | [Matrix][6]       | Some transform.              |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Graphics.md
[2]: ../ImageSource.md
[3]: ../IntRectangle.md
[4]: ../Rectangle.md
[5]: ../Vector.md
[6]: ../Matrix.md
