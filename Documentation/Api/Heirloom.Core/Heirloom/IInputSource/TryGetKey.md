# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IInputSource.TryGetKey (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [IInputSource][1]

### TryGetKey(Key, out ButtonState)

Attempts to retreive the state of the specified key.

```cs
public abstract bool TryGetKey(Key key, out ButtonState state)
```

| Name  | Type             | Summary                                                       |
|-------|------------------|---------------------------------------------------------------|
| key   | [Key][2]         | Some key.                                                     |
| state | [ButtonState][3] | Outputs the current state of the key, if call was successful. |

> **Returns** - `bool` - True if the value was sucessfully retreived.

[0]: ../../../Heirloom.Core.md
[1]: ../IInputSource.md
[2]: ../Key.md
[3]: ../ButtonState.md
