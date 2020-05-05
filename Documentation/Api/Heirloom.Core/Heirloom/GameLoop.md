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

[Graphics][1], [IsRunning][2]

### Methods

[Start][3], [Stop][4], [Update][5]

### Static Methods

[Create][6]

## Properties

#### Instance

| Name           | Type                 | Summary                             |
|----------------|----------------------|-------------------------------------|
| [Graphics][1]  | [GraphicsContext][7] | Gets the associated render context. |
| [IsRunning][2] | `bool`               | Is the render thread active?        |

## Methods

#### Instance

| Name                           | Return Type | Summary                                                                |
|--------------------------------|-------------|------------------------------------------------------------------------|
| [Start()][3]                   | `void`      | Start the render thread. This thread will automatically terminate w... |
| [Stop()][4]                    | `void`      | Stop the render thread.                                                |
| [Update(GraphicsContext...][5] | `void`      |                                                                        |

#### Static

| Name                           | Return Type   | Summary |
|--------------------------------|---------------|---------|
| [Create(GraphicsContext...][6] | [GameLoop][8] |         |

[0]: ../../Heirloom.Core.md
[1]: GameLoop/Graphics.md
[2]: GameLoop/IsRunning.md
[3]: GameLoop/Start.md
[4]: GameLoop/Stop.md
[5]: GameLoop/Update.md
[6]: GameLoop/Create.md
[7]: GraphicsContext.md
[8]: GameLoop.md
