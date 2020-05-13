# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Sprite (Class)

> **Namespace**: [Heirloom][0]

An animated sprite. A sprite is a collection of [SpriteAnimation][1] .

```cs
public sealed class Sprite
```

### Properties

[Animations][2]

### Methods

[AddAnimation][3], [ContainsAnimation][4], [GetAnimation][5], [RemoveAnimation][6]

## Properties

#### Instance

| Name            | Type                                           | Summary                                        |
|-----------------|------------------------------------------------|------------------------------------------------|
| [Animations][2] | `IReadOnlyDictionary<string, SpriteAnimation>` | Gets a read-only view of the animations table. |

## Methods

#### Instance

| Name                           | Return Type          | Summary                                                     |
|--------------------------------|----------------------|-------------------------------------------------------------|
| [AddAnimation(SpriteAni...][3] | `void`               | Adds an animation to this sprite.                           |
| [ContainsAnimation(string)][4] | `bool`               | Determines if this sprite contains the specified animation. |
| [GetAnimation(string)][5]      | [SpriteAnimation][1] | Gets an animation contained by this sprite.                 |
| [RemoveAnimation(Sprite...][6] | `bool`               | Removes an animation from this sprite.                      |

[0]: ../../Heirloom.Core.md
[1]: SpriteAnimation.md
[2]: Sprite/Animations.md
[3]: Sprite/AddAnimation.md
[4]: Sprite/ContainsAnimation.md
[5]: Sprite/GetAnimation.md
[6]: Sprite/RemoveAnimation.md
