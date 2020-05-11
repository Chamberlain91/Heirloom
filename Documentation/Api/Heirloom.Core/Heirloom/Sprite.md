# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Sprite (Class)

> **Namespace**: [Heirloom][0]

A representation of an animated sprite. May also contains per-frame and animation sequence information for animating the sprite.

```cs
public sealed class Sprite
```

### Properties

[Animations][1]

### Methods

[AddAnimation][2], [ContainsAnimation][3], [GetAnimation][4], [RemoveAnimation][5]

## Properties

#### Instance

| Name            | Type                                           | Summary                                        |
|-----------------|------------------------------------------------|------------------------------------------------|
| [Animations][1] | `IReadOnlyDictionary<string, SpriteAnimation>` | Gets a read-only view of the animations table. |

## Methods

#### Instance

| Name                           | Return Type          | Summary                                                     |
|--------------------------------|----------------------|-------------------------------------------------------------|
| [AddAnimation(SpriteAni...][2] | `void`               | Adds an animation to this sprite.                           |
| [ContainsAnimation(string)][3] | `bool`               | Determines if this sprite contains the specified animation. |
| [GetAnimation(string)][4]      | [SpriteAnimation][6] | Gets an animation contained by this sprite.                 |
| [RemoveAnimation(Sprite...][5] | `bool`               | Removes an animation from this sprite.                      |

[0]: ../../Heirloom.Core.md
[1]: Sprite/Animations.md
[2]: Sprite/AddAnimation.md
[3]: Sprite/ContainsAnimation.md
[4]: Sprite/GetAnimation.md
[5]: Sprite/RemoveAnimation.md
[6]: SpriteAnimation.md
