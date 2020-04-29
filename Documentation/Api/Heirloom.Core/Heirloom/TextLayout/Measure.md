# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## TextLayout.Measure

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [TextLayout][1]  

### Measure(string, Font, int)

Computes the bounding box that the specified text will occupy within an infinite layout size.

```cs
public static Rectangle Measure(string text, Font font, int fontSize)
```

### Measure(string, in Size, Font, int)

Computes the bounding box that the specified text will occupy within the given layout size.

```cs
public static Rectangle Measure(string text, in Size layoutSize, Font font, int fontSize)
```

### Measure(string, in Rectangle, Font, int)

Computes the bounding box that the specified text will occupy within the given layout size.

```cs
public static Rectangle Measure(string text, in Rectangle layoutBox, Font font, int fontSize)
```

[0]: ../../../Heirloom.Core.md
[1]: ../TextLayout.md
