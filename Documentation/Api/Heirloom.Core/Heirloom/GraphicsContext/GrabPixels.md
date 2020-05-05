# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GraphicsContext.GrabPixels (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [GraphicsContext][1]

### GrabPixels(IntRectangle)

Grab the pixels from a subregion of the current surface and return that image. (ie, a screenshot)

```cs
public abstract Image GrabPixels(IntRectangle region)
```

| Name   | Type              | Summary                                    |
|--------|-------------------|--------------------------------------------|
| region | [IntRectangle][2] | A region within the currently set surface. |

> **Returns** - [Image][3] - An image with a copy of the pixels on the surface.

### GrabPixels()

Grab the pixels from the current surface and return that image. (ie, a screenshot)

```cs
public Image GrabPixels()
```

> **Returns** - [Image][3] - An image with a copy of the pixels on the surface.

[0]: ../../../Heirloom.Core.md
[1]: ../GraphicsContext.md
[2]: ../IntRectangle.md
[3]: ../Image.md
