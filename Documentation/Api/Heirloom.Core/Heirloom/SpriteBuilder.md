# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## SpriteBuilder (Class)

> **Namespace**: [Heirloom][0]

Utility object for manually constructing a sprite and its animations from images.

```cs
public sealed class SpriteBuilder : IEnumerable
```

### Inherits

IEnumerable

### Methods

[Add][1], [Clear][2], [CreateSprite][3]

## Methods

#### Instance

| Name                           | Return Type | Summary                                                   |
|--------------------------------|-------------|-----------------------------------------------------------|
| [Add(string, Image)][1]        | `void`      | Add a single image animation.                             |
| [Add(string, float, par...][1] | `void`      |                                                           |
| [Add(string, float, IEn...][1] | `void`      | Adds a new animation to the builder from multiple images. |
| [Add(string, float, Spr...][1] | `void`      |                                                           |
| [Add(string, float, Spr...][1] | `void`      |                                                           |
| [Clear()][2]                   | `void`      | Clears all frames and animations.                         |
| [CreateSprite()][3]            | [Sprite][4] | Create a sprite the current state of the builder.         |

[0]: ../../Heirloom.Core.md
[1]: SpriteBuilder/Add.md
[2]: SpriteBuilder/Clear.md
[3]: SpriteBuilder/CreateSprite.md
[4]: Sprite.md
