# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Image.Copy (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Image][1]

### Copy(Image, in IntRectangle, Image, in IntVector)

```cs
public static void Copy(Image source, in IntRectangle sourceRegion, Image target, in IntVector targetOffset)
```

| Name         | Type              | Summary |
|--------------|-------------------|---------|
| source       | [Image][1]        |         |
| sourceRegion | [IntRectangle][2] |         |
| target       | [Image][1]        |         |
| targetOffset | [IntVector][3]    |         |

> **Returns** - `void`

### Copy(Image, in IntRectangle, ColorBytes*, int, in IntVector)

```cs
public static void Copy(Image source, in IntRectangle sourceRegion, ColorBytes* target, int targetWidth, in IntVector targetOffset)
```

| Name         | Type              | Summary |
|--------------|-------------------|---------|
| source       | [Image][1]        |         |
| sourceRegion | [IntRectangle][2] |         |
| target       | [ColorBytes*][4]  |         |
| targetWidth  | `int`             |         |
| targetOffset | [IntVector][3]    |         |

> **Returns** - `void`

### Copy(ColorBytes*, int, in IntRectangle, Image, in IntVector)

```cs
public static void Copy(ColorBytes* sourcePtr, int sourceWidth, in IntRectangle sourceRegion, Image target, in IntVector targetOffset)
```

| Name         | Type              | Summary |
|--------------|-------------------|---------|
| sourcePtr    | [ColorBytes*][4]  |         |
| sourceWidth  | `int`             |         |
| sourceRegion | [IntRectangle][2] |         |
| target       | [Image][1]        |         |
| targetOffset | [IntVector][3]    |         |

> **Returns** - `void`

### Copy(ColorBytes*, int, in IntRectangle, ColorBytes*, int, in IntVector)

```cs
public static void Copy(ColorBytes* source, int sourceWidth, in IntRectangle sourceRegion, ColorBytes* target, int targetWidth, in IntVector targetOffset)
```

| Name         | Type              | Summary |
|--------------|-------------------|---------|
| source       | [ColorBytes*][4]  |         |
| sourceWidth  | `int`             |         |
| sourceRegion | [IntRectangle][2] |         |
| target       | [ColorBytes*][4]  |         |
| targetWidth  | `int`             |         |
| targetOffset | [IntVector][3]    |         |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Image.md
[2]: ../IntRectangle.md
[3]: ../IntVector.md
[4]: ../ColorBytes_.md
