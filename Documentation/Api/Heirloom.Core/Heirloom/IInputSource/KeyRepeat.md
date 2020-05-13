# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IInputSource.KeyRepeat (Event)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [IInputSource][1]

#### KeyRepeat

An event raised when a button on the keyboard was 'repeated'.

```cs
public Action<Screen, KeyEvent> KeyRepeat { add; remove; }
```

Type: `Action<Screen, KeyEvent>`

This usually occurs from holding the key down for a period of time.

[0]: ../../../Heirloom.Core.md
[1]: ../IInputSource.md
