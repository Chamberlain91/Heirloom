# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## SpriteBuilder.Add (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [SpriteBuilder][1]

### Add(string, Image)

Add a single image animation.

```cs
public void Add(string name, Image frame)
```

| Name  | Type       | Summary             |
|-------|------------|---------------------|
| name  | `string`   | The animation name. |
| frame | [Image][2] | Some image.         |

> **Returns** - `void`

### Add(string, float, params Image[])

```cs
public void Add(string name, float frameDelay, params Image[] frames)
```

| Name       | Type         | Summary |
|------------|--------------|---------|
| name       | `string`     |         |
| frameDelay | `float`      |         |
| frames     | [Image[]][2] |         |

> **Returns** - `void`

### Add(string, float, IEnumerable<Image>)

Adds a new animation to the builder from multiple images.

```cs
public void Add(string name, float frameDelay, IEnumerable<Image> frames)
```

| Name       | Type                  | Summary                              |
|------------|-----------------------|--------------------------------------|
| name       | `string`              | The animation name.                  |
| frameDelay | `float`               | The delay between frames in seconds. |
| frames     | `IEnumerable\<Image>` | The image sequence to animate with.  |

> **Returns** - `void`

### Add(string, float, Sprite.Direction, params Image[])

```cs
public void Add(string name, float frameDelay, Sprite.Direction direction, params Image[] frames)
```

| Name       | Type                  | Summary |
|------------|-----------------------|---------|
| name       | `string`              |         |
| frameDelay | `float`               |         |
| direction  | [Sprite.Direction][3] |         |
| frames     | [Image[]][2]          |         |

> **Returns** - `void`

### Add(string, float, Sprite.Direction, IEnumerable<Image>)

```cs
public void Add(string name, float frameDelay, Sprite.Direction direction, IEnumerable<Image> frames)
```

| Name       | Type                  | Summary |
|------------|-----------------------|---------|
| name       | `string`              |         |
| frameDelay | `float`               |         |
| direction  | [Sprite.Direction][3] |         |
| frames     | `IEnumerable\<Image>` |         |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../SpriteBuilder.md
[2]: ../Image.md
[3]: ../Sprite.Direction.md
