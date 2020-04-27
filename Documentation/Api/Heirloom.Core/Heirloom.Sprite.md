# Sprite

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

A representation of single animated sprite. May also contains per-frame and animation sequence information for animating the sprite.

```cs
public sealed class Sprite
```

--------------------------------------------------------------------------------

**Properties**: [Frames][1], [Animations][2], [DefaultAnimation][3]

**Methods**: [GetAnimation][4]

--------------------------------------------------------------------------------

## Constructors

### Sprite(SpriteBuilder)

```cs
Sprite(SpriteBuilder builder)
```

### Sprite(string)

Constructs a new sprite from the specified file path resolved by [Files.OpenStream][5] .

```cs
public Sprite(string path)
```

### Sprite(Stream)

Constructs a new sprite from a stream (ie, an Aseprite file or other supported format).

```cs
public Sprite(Stream stream)
```

### Sprite(Image)

Constructs a new sprite from a single image.

```cs
public Sprite(Image image)
```

## Properties

| Name                  | Summary                                                   |
|-----------------------|-----------------------------------------------------------|
| [Frames][1]           | Gets a read-only list of frames contained by this sprite. |
| [Animations][2]       | Gets the name of each known animation sequence.           |
| [DefaultAnimation][3] | Gets the default animation.                               |

## Methods

| Name              | Summary                             |
|-------------------|-------------------------------------|
| [GetAnimation][4] | Gets an animation sequence by name. |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Sprite.Frames.md
[2]: Heirloom.Sprite.Animations.md
[3]: Heirloom.Sprite.DefaultAnimation.md
[4]: Heirloom.Sprite.GetAnimation.md
[5]: Heirloom.Files.OpenStream.md
