# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IntRectangle.Merge (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [IntRectangle][1]

### Merge(in IntRectangle, in IntRectangle)

Merges the given rectangles into one potentially larger rectangle.

```cs
public static IntRectangle Merge(in IntRectangle a, in IntRectangle b)
```

| Name | Type              | Summary                 |
|------|-------------------|-------------------------|
| a    | [IntRectangle][1] | Some rectangle ' `a` '. |
| b    | [IntRectangle][1] | Some rectangle ' `b` '. |

> **Returns** - [IntRectangle][1] - A potentially larger rectangle comprised of the two given.

Useful for computing a bounding rectangle.

### Merge(params IntRectangle[])

```cs
public static IntRectangle Merge(params IntRectangle[] rects)
```

| Name  | Type                | Summary |
|-------|---------------------|---------|
| rects | [IntRectangle[]][1] |         |

> **Returns** - [IntRectangle][1]

[0]: ../../../Heirloom.Core.md
[1]: ../IntRectangle.md
