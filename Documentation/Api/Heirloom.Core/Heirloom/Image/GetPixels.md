# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Image.GetPixels (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Image][1]

### GetPixels()

Gets a copy of all the pixels in this image.

```cs
public ColorBytes[] GetPixels()
```

> **Returns** - [ColorBytes[]][2] - A newly allocated copy of the pixels.

### GetPixels(Span<ColorBytes>)

Copies the image pixels into an already allocated buffer.

```cs
public void GetPixels(Span<ColorBytes> buffer)
```

| Name   | Type               | Summary                            |
|--------|--------------------|------------------------------------|
| buffer | `Span<ColorBytes>` | A span of the buffer to copy into. |

> **Returns** - `void` - A newly allocated copy of the pixels.

### GetPixels(ColorBytes[], int)

```cs
public void GetPixels(ColorBytes[] buffer, int offset)
```

| Name   | Type              | Summary |
|--------|-------------------|---------|
| buffer | [ColorBytes[]][2] |         |
| offset | `int`             |         |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Image.md
[2]: ../ColorBytes.md
