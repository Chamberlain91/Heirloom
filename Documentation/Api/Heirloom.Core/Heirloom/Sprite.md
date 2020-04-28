# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Sprite

> **Namespace**: [Heirloom][0]  

A representation of single animated sprite. May also contains per-frame and animation sequence information for animating the sprite.

```cs
public sealed class Sprite
```

#### Properties

[Frames][1], [Animations][2], [DefaultAnimation][3]

#### Methods

[GetAnimation][4]

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

[0]: ../../Heirloom.Core.md
[1]: Sprite/Frames.md
[2]: Sprite/Animations.md
[3]: Sprite/DefaultAnimation.md
[4]: Sprite/GetAnimation.md
