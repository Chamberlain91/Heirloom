# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## ILogHandler (Interface)

> **Namespace**: [Heirloom][0]

Represents the interface for handling log messages from [Log][1] .

```cs
public interface ILogHandler
```

### Methods

[Debug][2], [Error][3], [Info][4], [Warning][5]

## Methods

#### Instance

| Name                 | Return Type | Summary                     |
|----------------------|-------------|-----------------------------|
| [Debug(object)][2]   | `void`      | Logs a debug message.       |
| [Error(object)][3]   | `void`      | Logs an error message.      |
| [Info(object)][4]    | `void`      | Logs a information message. |
| [Warning(object)][5] | `void`      | Logs a warning message.     |

[0]: ../../Heirloom.Core.md
[1]: Log.md
[2]: ILogHandler/Debug.md
[3]: ILogHandler/Error.md
[4]: ILogHandler/Info.md
[5]: ILogHandler/Warning.md
