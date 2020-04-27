# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## IntRectangle.Offset

> **Namespace**: [Heirloom][0]  
> **Type**: [IntRectangle][1]  

### Offset(int, int)

Translates this rectangle.

```cs
public void Offset(int x, int y)
```

### Offset(IntVector)

Translates this rectangle.

```cs
public void Offset(IntVector offset)
```

### Offset(IntRectangle, int, int)

Copies and translates the given rectangle.

```cs
public static IntRectangle Offset(IntRectangle rect, int x, int y)
```

### Offset(IntRectangle, IntVector)

Copies and translates the given rectangle.

```cs
public static IntRectangle Offset(IntRectangle rect, IntVector offset)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.IntRectangle.md
