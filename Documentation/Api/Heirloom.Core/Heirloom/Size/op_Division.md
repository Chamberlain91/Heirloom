# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Size.Division (Operator)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Size][1]

### Division(Size, Size)

Performs the component-wise division of two size structures.

```cs
public static Size Division(Size a, Size b)
```

| Name | Type      | Summary |
|------|-----------|---------|
| a    | [Size][1] |         |
| b    | [Size][1] |         |

> **Returns** - [Size][1]

### Division(Size, float)

Performs the component-wise scaling of a size structure via division.

```cs
public static Size Division(Size a, float v)
```

| Name | Type      | Summary |
|------|-----------|---------|
| a    | [Size][1] |         |
| v    | `float`   |         |

> **Returns** - [Size][1]

### Division(float, Size)

Performs the component-wise scaling of a size structure via division.

```cs
public static Size Division(float v, Size a)
```

| Name | Type      | Summary |
|------|-----------|---------|
| v    | `float`   |         |
| a    | [Size][1] |         |

> **Returns** - [Size][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Size.md
