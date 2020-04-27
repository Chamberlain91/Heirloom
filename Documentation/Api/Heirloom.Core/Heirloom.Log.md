# Log

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Provides a simple mechanism to log debug and info messages.

```cs
public static class Log
```

--------------------------------------------------------------------------------

**Static Properties**: [LogHandler][1]

**Static Methods**: [SetVerbosity][2], [Debug][3], [Warning][4], [Info][5], [Error][6]

--------------------------------------------------------------------------------

## Properties

| Name            | Summary                               |
|-----------------|---------------------------------------|
| [LogHandler][1] | Gets or sets the current log handler. |

## Methods

| Name              | Summary                                           |
|-------------------|---------------------------------------------------|
| [SetVerbosity][2] | Sets the verbosity level of the calling assembly. |
| [SetVerbosity][2] | Sets the verbosity level of a specific assembly.  |
| [SetVerbosity][2] | Sets the verbosity level of a specific assembly.  |
| [Debug][3]        | Logs a debug message.                             |
| [Warning][4]      | Logs a warning message.                           |
| [Info][5]         | Logs a error message.                             |
| [Error][6]        | Logs a error message.                             |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Log.LogHandler.md
[2]: Heirloom.Log.SetVerbosity.md
[3]: Heirloom.Log.Debug.md
[4]: Heirloom.Log.Warning.md
[5]: Heirloom.Log.Info.md
[6]: Heirloom.Log.Error.md
