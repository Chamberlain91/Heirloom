# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Screen.KeyRepeat (Event)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Screen][1]

#### KeyRepeat

An event raised when a key has been 'repeated' on the keyboard.

```cs
public Action<Screen, KeyEvent> KeyRepeat { add; remove; }
```

Type: `Action<Screen, KeyEvent>`

This occurs when holding the key for an extended time.

[0]: ../../../Heirloom.Core.md
[1]: ../Screen.md
