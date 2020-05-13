# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## SpritePlayer (Class)

> **Namespace**: [Heirloom][0]

A utility class to help drive sprite based animation.

```cs
public sealed class SpritePlayer
```

**See Also:** [Sprite][1], [SpriteAnimation][2]

### Properties

[Animation][3], [FrameNumber][4], [Image][5], [IsPlaying][6], [Sprite][1]

### Methods

[Play][7], [Stop][8], [Update][9]

## Properties

#### Instance

| Name             | Type                 | Summary                                                                |
|------------------|----------------------|------------------------------------------------------------------------|
| [Animation][3]   | [SpriteAnimation][2] | Gets the active animation.                                             |
| [FrameNumber][4] | `int`                | Gets which frame number the player is currently at.                    |
| [Image][5]       | [Image][10]          | Gets the image for the current frame of the active animation.          |
| [IsPlaying][6]   | `bool`               | Gets a value that determines if the player is performing playback o... |
| [Sprite][1]      | [Sprite][11]         | Gets the Sprite the player is reponsible for.                          |

## Methods

#### Instance

| Name               | Return Type | Summary                                                  |
|--------------------|-------------|----------------------------------------------------------|
| [Play(string)][7]  | `void`      | Begins playback of the specified animation.              |
| [Play()][7]        | `void`      | Resumes playback of the active animation.                |
| [Stop()][8]        | `void`      | Stops playback of the active animation.                  |
| [Update(float)][9] | `void`      | Updates the player, advancing animation by elapsed time. |

[0]: ../../Heirloom.Core.md
[1]: SpritePlayer/Sprite.md
[2]: SpriteAnimation.md
[3]: SpritePlayer/Animation.md
[4]: SpritePlayer/FrameNumber.md
[5]: SpritePlayer/Image.md
[6]: SpritePlayer/IsPlaying.md
[7]: SpritePlayer/Play.md
[8]: SpritePlayer/Stop.md
[9]: SpritePlayer/Update.md
[10]: Image.md
[11]: Sprite.md
