# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## LogVerbosity (Enum)

> **Namespace**: [Heirloom][0]

Controls the verbosity of [Log][1] .

```cs
public enum LogVerbosity : IComparable, IFormattable, IConvertible
```

| Name    | Summary                                              |
|---------|------------------------------------------------------|
| Debug   | All messages are processed.                          |
| Error   | Only error messages are processed.                   |
| Info    | Only error and info logs are processed.              |
| None    | No messages are processed.                           |
| Warning | Only error, info and warning messages are processed. |

[0]: ../../Heirloom.Core.md
[1]: Log.md
