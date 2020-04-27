# Graphics.DrawSubImage

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Graphics][1]  

--------------------------------------------------------------------------------

### DrawSubImage(ImageSource, in IntRectangle, in Rectangle)

Draws a sub-region of an image stretched to fill a rectangular region to the current surface ignoring the image origin offset.

```cs
public void DrawSubImage(ImageSource image, in IntRectangle region, in Rectangle rectangle)
```

### DrawSubImage(ImageSource, in IntRectangle, in Vector)

Draws a sub-region of an image to the current surface ignoring the image origin offset.

```cs
public void DrawSubImage(ImageSource image, in IntRectangle region, in Vector position)
```

### DrawSubImage(ImageSource, in IntRectangle, in Matrix)

Draws a sub-region of an image to the current surface ignoring the image origin offset.

```cs
public void DrawSubImage(ImageSource image, in IntRectangle region, in Matrix transform)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.Graphics.md
