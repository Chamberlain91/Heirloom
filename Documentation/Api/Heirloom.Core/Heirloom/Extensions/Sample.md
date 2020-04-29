# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Extensions.Sample (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Extensions][1]

### Sample(INoise1D, float, int, float)

Sample one-dimensional octave noise.

```cs
public static float Sample(INoise1D noise, float x, int octaves, float persistence = 0.5)
```

`ExtensionAttribute`

| Name        | Type          | Summary |
|-------------|---------------|---------|
| noise       | [INoise1D][2] |         |
| x           | `float`       |         |
| octaves     | `int`         |         |
| persistence | `float`       |         |

> **Returns** - `float`

### Sample(INoise2D, in Vector)

Sample two-dimensional noise.

```cs
public static float Sample(INoise2D noise, in Vector position)
```

`ExtensionAttribute`

| Name     | Type          | Summary |
|----------|---------------|---------|
| noise    | [INoise2D][3] |         |
| position | [Vector][4]   |         |

> **Returns** - `float`

### Sample(INoise2D, in Vector, int, float)

Sample two-dimensional octave noise.

```cs
public static float Sample(INoise2D noise, in Vector position, int octaves, float persistence = 0.5)
```

`ExtensionAttribute`

| Name        | Type          | Summary |
|-------------|---------------|---------|
| noise       | [INoise2D][3] |         |
| position    | [Vector][4]   |         |
| octaves     | `int`         |         |
| persistence | `float`       |         |

> **Returns** - `float`

### Sample(INoise2D, float, float, int, float)

Sample two-dimensional octave noise.

```cs
public static float Sample(INoise2D noise, float x, float y, int octaves, float persistence = 0.5)
```

`ExtensionAttribute`

| Name        | Type          | Summary |
|-------------|---------------|---------|
| noise       | [INoise2D][3] |         |
| x           | `float`       |         |
| y           | `float`       |         |
| octaves     | `int`         |         |
| persistence | `float`       |         |

> **Returns** - `float`

### Sample(INoise3D, float, float, float, int, float)

Sample three-dimensional octave noise.

```cs
public static float Sample(INoise3D noise, float x, float y, float z, int octaves, float persistence = 0.5)
```

`ExtensionAttribute`

| Name        | Type          | Summary |
|-------------|---------------|---------|
| noise       | [INoise3D][5] |         |
| x           | `float`       |         |
| y           | `float`       |         |
| z           | `float`       |         |
| octaves     | `int`         |         |
| persistence | `float`       |         |

> **Returns** - `float`

[0]: ../../../Heirloom.Core.md
[1]: ../Extensions.md
[2]: ../INoise1D.md
[3]: ../INoise2D.md
[4]: ../Vector.md
[5]: ../INoise3D.md
