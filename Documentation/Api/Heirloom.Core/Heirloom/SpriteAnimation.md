# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## SpriteAnimation (Class)

> **Namespace**: [Heirloom][0]

Represents an image based per frame animation.

```cs
public class SpriteAnimation : IReadOnlyList<SpriteFrame>, IReadOnlyCollection<SpriteFrame>, IEnumerable<SpriteFrame>, IEnumerable
```

**See Also:** [SpritePlayer][1]

### Inherits

IReadOnlyList\<SpriteFrame>, IReadOnlyCollection\<SpriteFrame>, IEnumerable\<SpriteFrame>, IEnumerable

### Properties

[Direction][2], [Duration][3], [Indexer][4], [Length][5], [Name][6]

### Methods

[Add][7], [GetEnumerator][8], [Insert][9], [RemoveAt][10]

## Properties

#### Instance

| Name           | Type                     | Summary                                        |
|----------------|--------------------------|------------------------------------------------|
| [Direction][2] | [AnimationDirection][11] | Gets or sets the intended animation direction. |
| [Duration][3]  | `float`                  | Gets the duration of the animation in seconds. |
| [Indexer][4]   | [SpriteFrame][12]        |                                                |
| [Length][5]    | `int`                    | Gets the number of frames in the animation.    |
| [Name][6]      | `string`                 | The name of the animation sequence.            |

## Methods

#### Instance

| Name                           | Return Type                | Summary                                                                |
|--------------------------------|----------------------------|------------------------------------------------------------------------|
| [Add(Image, float)][7]         | `void`                     | Adds a new frame to the end of the animation.                          |
| [GetEnumerator()][8]           | `IEnumerator<SpriteFrame>` | Returns an enumerator that iterates through the frames of the anima... |
| [Insert(int, Image, float)][9] | `void`                     | Inserts a new frame to the animation before the specified frame num... |
| [RemoveAt(int)][10]            | `void`                     | Removes a frame at the specified frame number.                         |

[0]: ../../Heirloom.Core.md
[1]: SpritePlayer.md
[2]: SpriteAnimation/Direction.md
[3]: SpriteAnimation/Duration.md
[4]: SpriteAnimation/Indexer.md
[5]: SpriteAnimation/Length.md
[6]: SpriteAnimation/Name.md
[7]: SpriteAnimation/Add.md
[8]: SpriteAnimation/GetEnumerator.md
[9]: SpriteAnimation/Insert.md
[10]: SpriteAnimation/RemoveAt.md
[11]: AnimationDirection.md
[12]: SpriteFrame.md
