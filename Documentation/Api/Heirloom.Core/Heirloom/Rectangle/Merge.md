# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Rectangle.Merge (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Rectangle][1]

### Merge(in Rectangle, in Rectangle)

Merges the given rectangles into one potentially larger rectangle.

```cs
public static Rectangle Merge(in Rectangle a, in Rectangle b)
```

| Name | Type           | Summary                 |
|------|----------------|-------------------------|
| a    | [Rectangle][1] | Some rectangle ' `a` '. |
| b    | [Rectangle][1] | Some rectangle ' `b` '. |

> **Returns** - [Rectangle][1] - A potentially larger rectangle comprised of the two given.

Useful for computing a bounding rectangle.

### Merge(params Rectangle[])

```cs
public static Rectangle Merge(params Rectangle[] rects)
```

| Name  | Type             | Summary |
|-------|------------------|---------|
| rects | [Rectangle[]][1] |         |

> **Returns** - [Rectangle][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Rectangle.md
