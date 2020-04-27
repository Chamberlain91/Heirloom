# Graphics.DrawText

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Graphics][1]  

--------------------------------------------------------------------------------

### DrawText(StyledText, in Vector, Font, int, TextAlign)

Draws rich text to the current surface.

```cs
public void DrawText(StyledText styledText, in Vector position, Font font, int size, TextAlign align = Left)
```

### DrawText(StyledText, in Rectangle, Font, int, TextAlign)

Draws rich text to the current surface.

```cs
public void DrawText(StyledText styledText, in Rectangle bounds, Font font, int size, TextAlign align = Left)
```

### DrawText(string, in Vector, Font, int, DrawTextCallback)

Draws text to the current surface.

```cs
public void DrawText(string text, in Vector position, Font font, int size, DrawTextCallback callback)
```

### DrawText(string, in Vector, Font, int, TextAlign, DrawTextCallback)

Draws text to the current surface.

```cs
public void DrawText(string text, in Vector position, Font font, int size, TextAlign align = Left, DrawTextCallback callback = null)
```

### DrawText(string, in Rectangle, Font, int, DrawTextCallback)

Draws text to the current surface.

```cs
public void DrawText(string text, in Rectangle bounds, Font font, int size, DrawTextCallback callback)
```

### DrawText(string, in Rectangle, Font, int, TextAlign, DrawTextCallback)

Draws text to the current surface.

```cs
public void DrawText(string text, in Rectangle bounds, Font font, int size, TextAlign align = Left, DrawTextCallback callback = null)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.Graphics.md
