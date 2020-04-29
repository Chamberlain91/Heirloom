# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Sprite (Class)

> **Namespace**: [Heirloom][0]

A representation of single animated sprite. May also contains per-frame and animation sequence information for animating the sprite.

```cs
public sealed class Sprite
```

### Properties

[Animations][1], [DefaultAnimation][2], [Frames][3]

### Methods

[GetAnimation][4]

## Properties

#### Instance

| Name                  | Type                               | Summary                                                   |
|-----------------------|------------------------------------|-----------------------------------------------------------|
| [Animations][1]       | `IReadOnlyCollection\<string>`     | Gets the name of each known animation sequence.           |
| [DefaultAnimation][2] | [Sprite.Animation][5]              | Gets the default animation.                               |
| [Frames][3]           | `IReadOnlyList\<Sprite.FrameInfo>` | Gets a read-only list of frames contained by this sprite. |

## Methods

#### Instance

| Name                      | Return Type           | Summary                             |
|---------------------------|-----------------------|-------------------------------------|
| [GetAnimation(string)][4] | [Sprite.Animation][5] | Gets an animation sequence by name. |

[0]: ../../Heirloom.Core.md
[1]: Sprite/Animations.md
[2]: Sprite/DefaultAnimation.md
[3]: Sprite/Frames.md
[4]: Sprite/GetAnimation.md
[5]: Sprite.Animation.md
