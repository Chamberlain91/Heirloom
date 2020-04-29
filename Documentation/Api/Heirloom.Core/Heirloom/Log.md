# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Log (Class)

> **Namespace**: [Heirloom][0]

Provides a simple mechanism to log debug and info messages.

```cs
public static class Log
```

### Static Properties

[LogHandler][1]

### Static Methods

[Debug][2], [Error][3], [Info][4], [SetVerbosity][5], [Warning][6]

## Properties

| Name            | Type             | Summary                               |
|-----------------|------------------|---------------------------------------|
| [LogHandler][1] | [ILogHandler][7] | Gets or sets the current log handler. |

## Methods

| Name                           | Return Type | Summary                                           |
|--------------------------------|-------------|---------------------------------------------------|
| [Debug(object)][2]             | `void`      | Logs a debug message.                             |
| [Error(object)][3]             | `void`      | Logs a error message.                             |
| [Info(object)][4]              | `void`      | Logs a error message.                             |
| [SetVerbosity(LogVerbos...][5] | `void`      | Sets the verbosity level of the calling assembly. |
| [SetVerbosity(LogVerbos...][5] | `void`      | Sets the verbosity level of a specific assembly.  |
| [SetVerbosity(LogVerbos...][5] | `void`      | Sets the verbosity level of a specific assembly.  |
| [Warning(object)][6]           | `void`      | Logs a warning message.                           |

[0]: ../../Heirloom.Core.md
[1]: Log/LogHandler.md
[2]: Log/Debug.md
[3]: Log/Error.md
[4]: Log/Info.md
[5]: Log/SetVerbosity.md
[6]: Log/Warning.md
[7]: ILogHandler.md
