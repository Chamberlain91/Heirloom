# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GraphicsContext.DrawImage (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [GraphicsContext][1]

### DrawImage(ImageSource, in Vector)

Draws an image to the current surface.

```cs
public void DrawImage(ImageSource image, in Vector position)
```

| Name     | Type             | Summary                    |
|----------|------------------|----------------------------|
| image    | [ImageSource][2] | Some image.                |
| position | [Vector][3]      | The position of the image. |

> **Returns** - `void`

### DrawImage(ImageSource, in Vector, float)

Draws an image to the current surface.

```cs
public void DrawImage(ImageSource image, in Vector position, float rotation)
```

| Name     | Type             | Summary                            |
|----------|------------------|------------------------------------|
| image    | [ImageSource][2] | Some image.                        |
| position | [Vector][3]      | The position of the image.         |
| rotation | `float`          | The rotation applied to the image. |

> **Returns** - `void`

### DrawImage(ImageSource, in Vector, float, in Vector)

Draws an image to the current surface.

```cs
public void DrawImage(ImageSource image, in Vector position, float rotation, in Vector scale)
```

| Name     | Type             | Summary                            |
|----------|------------------|------------------------------------|
| image    | [ImageSource][2] | Some image.                        |
| position | [Vector][3]      | The position of the image.         |
| rotation | `float`          | The rotation applied to the image. |
| scale    | [Vector][3]      | The scale applied to the image.    |

> **Returns** - `void`

### DrawImage(ImageSource, in Rectangle)

Draws an image stretched to fill a rectangular region to the current surface ignoring the image origin offset.

```cs
public void DrawImage(ImageSource image, in Rectangle rectangle)
```

| Name      | Type             | Summary                        |
|-----------|------------------|--------------------------------|
| image     | [ImageSource][2] | Some image.                    |
| rectangle | [Rectangle][4]   | The bounds of the drawn image. |

> **Returns** - `void`

### DrawImage(ImageSource, in Matrix)

Draws an image to the current surface.

```cs
public void DrawImage(ImageSource image, in Matrix transform)
```

| Name      | Type             | Summary         |
|-----------|------------------|-----------------|
| image     | [ImageSource][2] | Some image.     |
| transform | [Matrix][5]      | Some transform. |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../GraphicsContext.md
[2]: ../ImageSource.md
[3]: ../Vector.md
[4]: ../Rectangle.md
[5]: ../Matrix.md
