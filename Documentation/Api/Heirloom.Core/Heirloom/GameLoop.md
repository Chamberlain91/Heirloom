# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## GameLoop Class

> **Namespace**: [Heirloom][0]  

Provides a thread to manage invoking a render/update function continuously.

```cs
public abstract class GameLoop
```

#### Properties

[Graphics][1], [IsRunning][2]

#### Methods

[Update][3], [Start][4], [Stop][5]

#### Static Methods

[Create][6]

## Properties

| Name           | Summary                             |
|----------------|-------------------------------------|
| [Graphics][1]  | Gets the associated render context. |
| [IsRunning][2] | Is the render thread active?        |

## Methods

| Name        | Summary                                                                                                            |
|-------------|--------------------------------------------------------------------------------------------------------------------|
| [Update][3] |                                                                                                                    |
| [Start][4]  | Start the render thread. This thread will automatically terminate when the associated graphics object is disposed. |
| [Stop][5]   | Stop the render thread.                                                                                            |
| [Create][6] |                                                                                                                    |

[0]: ../../Heirloom.Core.md
[1]: GameLoop/Graphics.md
[2]: GameLoop/IsRunning.md
[3]: GameLoop/Update.md
[4]: GameLoop/Start.md
[5]: GameLoop/Stop.md
[6]: GameLoop/Create.md
