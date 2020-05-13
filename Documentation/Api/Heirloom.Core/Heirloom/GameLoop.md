# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GameLoop (Class)

> **Namespace**: [Heirloom][0]

Provides a thread to manage invoking a render/update function continuously.

```cs
public abstract class GameLoop
```

### Properties

[FixedFrameRate][1], [Graphics][2], [IsRunning][3]

### Methods

[Start][4], [Stop][5], [Update][6]

### Static Methods

[Create][7]

## Properties

#### Instance

| Name                | Type                 | Summary                             |
|---------------------|----------------------|-------------------------------------|
| [FixedFrameRate][1] | `int`                | Gets or sets the fixed frame rate.  |
| [Graphics][2]       | [GraphicsContext][8] | Gets the associated render context. |
| [IsRunning][3]      | `bool`               | Is the render thread active?        |

## Methods

#### Instance

| Name                           | Return Type | Summary                                                                |
|--------------------------------|-------------|------------------------------------------------------------------------|
| [Start()][4]                   | `void`      | Start the render thread. This thread will automatically terminate w... |
| [Stop()][5]                    | `void`      | Stop the render thread.                                                |
| [Update(GraphicsContext...][6] | `void`      | Called every iteration of the game loop to update and/or render the... |

#### Static

| Name                           | Return Type   | Summary                                                                |
|--------------------------------|---------------|------------------------------------------------------------------------|
| [Create(GraphicsContext...][7] | [GameLoop][9] | Creates a render loop instance from the given context and method re... |

[0]: ../../Heirloom.Core.md
[1]: GameLoop/FixedFrameRate.md
[2]: GameLoop/Graphics.md
[3]: GameLoop/IsRunning.md
[4]: GameLoop/Start.md
[5]: GameLoop/Stop.md
[6]: GameLoop/Update.md
[7]: GameLoop/Create.md
[8]: GraphicsContext.md
[9]: GameLoop.md
