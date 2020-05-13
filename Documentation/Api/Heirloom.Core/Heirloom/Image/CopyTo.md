# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Image.CopyTo (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Image][1]

### CopyTo(in IntRectangle, Image, in IntVector)

Copies a region of this image to another.

```cs
public void CopyTo(in IntRectangle region, Image target, in IntVector targetOffset)
```

| Name         | Type              | Summary                                        |
|--------------|-------------------|------------------------------------------------|
| region       | [IntRectangle][2] | The region to copy.                            |
| target       | [Image][1]        | The target image to copy pixels to.            |
| targetOffset | [IntVector][3]    | The offset within the target image to copy to. |

> **Returns** - `void`

### CopyTo(Image, in IntVector)

Copies this image to another image.

```cs
public void CopyTo(Image target, in IntVector targetOffset)
```

| Name         | Type           | Summary                                        |
|--------------|----------------|------------------------------------------------|
| target       | [Image][1]     | The target image to copy pixels to.            |
| targetOffset | [IntVector][3] | The offset within the target image to copy to. |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Image.md
[2]: ../IntRectangle.md
[3]: ../IntVector.md
