# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## SpritePlayer (Class)

> **Namespace**: [Heirloom.Core.Drawing.Images][0]

A utility class to help drive sprite based animation.

```cs
public sealed class SpritePlayer
```

### Properties

[Animation][1], [FrameNumber][2], [Image][3], [IsPlaying][4], [Sprite][5]

### Methods

[Play][6], [Stop][7], [Update][8]

## Properties

#### Instance

| Name             | Type                 | Summary                                                                |
|------------------|----------------------|------------------------------------------------------------------------|
| [Animation][1]   | [SpriteAnimation][9] | Gets the active animation.                                             |
| [FrameNumber][2] | `int`                | Gets which frame number the player is currently at.                    |
| [Image][3]       | [Image][10]          | Gets the image for the current frame of the active animation.          |
| [IsPlaying][4]   | `bool`               | Gets a value that determines if the player is performing playback o... |
| [Sprite][5]      | [Sprite][11]         | Gets the Sprite the player is reponsible for.                          |

## Methods

#### Instance

| Name               | Return Type | Summary                                                  |
|--------------------|-------------|----------------------------------------------------------|
| [Play(string)][6]  | `void`      | Begins playback of the specified animation.              |
| [Play()][6]        | `void`      | Resumes playback of the active animation.                |
| [Stop()][7]        | `void`      | Stops playback of the active animation.                  |
| [Update(float)][8] | `void`      | Updates the player, advancing animation by elapsed time. |

[0]: ../../Heirloom.Core.md
[1]: SpritePlayer/Animation.md
[2]: SpritePlayer/FrameNumber.md
[3]: SpritePlayer/Image.md
[4]: SpritePlayer/IsPlaying.md
[5]: SpritePlayer/Sprite.md
[6]: SpritePlayer/Play.md
[7]: SpritePlayer/Stop.md
[8]: SpritePlayer/Update.md
[9]: ../Heirloom/SpriteAnimation.md
[10]: ../Heirloom/Image.md
[11]: ../Heirloom/Sprite.md
