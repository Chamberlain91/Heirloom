# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IInputSource.TryGetButton (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [IInputSource][1]

### TryGetButton(MouseButton, out ButtonState)

Attempts to retreive the state of the specified mouse button.

```cs
public abstract bool TryGetButton(MouseButton button, out ButtonState state)
```

| Name   | Type             | Summary                                                          |
|--------|------------------|------------------------------------------------------------------|
| button | [MouseButton][2] | The mouse button to query.                                       |
| state  | [ButtonState][3] | Outputs the current state of the button, if call was successful. |

> **Returns** - `bool` - True if the value was sucessfully retreived.

[0]: ../../../Heirloom.Core.md
[1]: ../IInputSource.md
[2]: ../MouseButton.md
[3]: ../ButtonState.md
