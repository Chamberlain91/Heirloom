# Image.Copy

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Image][1]  

--------------------------------------------------------------------------------

### Copy(Image, in IntRectangle, Image, in IntVector)

```cs
public void Copy(Image source, in IntRectangle sourceRegion, Image target, in IntVector targetOffset)
```

### Copy(Image, in IntRectangle, ColorBytes*, int, in IntVector)

```cs
public void Copy(Image source, in IntRectangle sourceRegion, ColorBytes* target, int targetWidth, in IntVector targetOffset)
```

### Copy(ColorBytes*, int, in IntRectangle, Image, in IntVector)

```cs
public void Copy(ColorBytes* sourcePtr, int sourceWidth, in IntRectangle sourceRegion, Image target, in IntVector targetOffset)
```

### Copy(ColorBytes*, int, in IntRectangle, ColorBytes*, int, in IntVector)

```cs
public void Copy(ColorBytes* source, int sourceWidth, in IntRectangle sourceRegion, ColorBytes* target, int targetWidth, in IntVector targetOffset)
```

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Image.md
