# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## SpriteAnimation (Class)

> **Namespace**: [Heirloom][0]

Represents an image based per frame animation.

```cs
public class SpriteAnimation : IReadOnlyList<SpriteFrame>, IReadOnlyCollection<SpriteFrame>, IEnumerable<SpriteFrame>, IEnumerable
```

### Inherits

IReadOnlyList\<SpriteFrame>, IReadOnlyCollection\<SpriteFrame>, IEnumerable\<SpriteFrame>, IEnumerable

### Properties

[Direction][1], [Duration][2], [Indexer][3], [Length][4], [Name][5]

### Methods

[Add][6], [GetEnumerator][7], [Insert][8], [RemoveAt][9]

## Properties

#### Instance

| Name           | Type                     | Summary                                        |
|----------------|--------------------------|------------------------------------------------|
| [Direction][1] | [AnimationDirection][10] | Gets or sets the intended animation direction. |
| [Duration][2]  | `float`                  | Gets the duration of the animation in seconds. |
| [Indexer][3]   | [SpriteFrame][11]        |                                                |
| [Length][4]    | `int`                    | Gets the number of frames in the animation.    |
| [Name][5]      | `string`                 | The name of the animation sequence.            |

## Methods

#### Instance

| Name                           | Return Type                | Summary                                                                |
|--------------------------------|----------------------------|------------------------------------------------------------------------|
| [Add(Image, float)][6]         | `void`                     | Adds a new frame to the end of the animation.                          |
| [GetEnumerator()][7]           | `IEnumerator<SpriteFrame>` | Returns an enumerator that iterates through the frames of the anima... |
| [Insert(int, Image, float)][8] | `void`                     | Inserts a new frame to the animation before the specified frame num... |
| [RemoveAt(int)][9]             | `void`                     | Removes a frame at the specified frame number.                         |

[0]: ../../Heirloom.Core.md
[1]: SpriteAnimation/Direction.md
[2]: SpriteAnimation/Duration.md
[3]: SpriteAnimation/Indexer.md
[4]: SpriteAnimation/Length.md
[5]: SpriteAnimation/Name.md
[6]: SpriteAnimation/Add.md
[7]: SpriteAnimation/GetEnumerator.md
[8]: SpriteAnimation/Insert.md
[9]: SpriteAnimation/RemoveAt.md
[10]: AnimationDirection.md
[11]: SpriteFrame.md
