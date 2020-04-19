# Heirloom.IO

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Log (Static Class)
<small>**Namespace**: Heirloom.IO</small>  

Provides a simple mechanism to log debug and info messages.

| Properties                 | Summary                               |
|----------------------------|---------------------------------------|
| [LogHandler](#LOGD1C530C8) | Gets or sets the current log handler. |

| Methods                      | Summary                                           |
|------------------------------|---------------------------------------------------|
| [SetVerbosity](#SET82783438) | Sets the verbosity level of the calling assembly. |
| [SetVerbosity](#SET85DA287A) | Sets the verbosity level of a specific assembly.  |
| [SetVerbosity](#SET6AF20EFB) | Sets the verbosity level of a specific assembly.  |
| [Debug](#DEBE83C33BA)        | Logs a debug message.                             |
| [Warning](#WARCC1C19B)       | Logs a warning message.                           |
| [Info](#INFC81040DB)         | Logs a error message.                             |
| [Error](#ERR4C00E9B5)        | Logs a error message.                             |

### Properties

#### <a name="LOGD1C530C8"></a>LogHandler : [ILogHandler](Heirloom.IO.ILogHandler.md)

<small>`Static`</small>

Gets or sets the current log handler.

### Methods

#### <a name="SET82783438"></a>SetVerbosity([LogVerbosity](Heirloom.IO.LogVerbosity.md) verbosity) : void
<small>`Static`</small>

Sets the verbosity level of the calling assembly.


#### <a name="SET85DA287A"></a>SetVerbosity([LogVerbosity](Heirloom.IO.LogVerbosity.md) verbosity, Assembly assembly) : void
<small>`Static`</small>

Sets the verbosity level of a specific assembly.


#### <a name="SET6AF20EFB"></a>SetVerbosity([LogVerbosity](Heirloom.IO.LogVerbosity.md) verbosity, string assembly) : void
<small>`Static`</small>

Sets the verbosity level of a specific assembly.


#### <a name="DEBE83C33BA"></a>Debug(object message) : void
<small>`Static`</small>

Logs a debug message.


#### <a name="WARCC1C19B"></a>Warning(object message) : void
<small>`Static`</small>

Logs a warning message.


#### <a name="INFC81040DB"></a>Info(object message) : void
<small>`Static`</small>

Logs a error message.


#### <a name="ERR4C00E9B5"></a>Error(object message) : void
<small>`Static`</small>

Logs a error message.


