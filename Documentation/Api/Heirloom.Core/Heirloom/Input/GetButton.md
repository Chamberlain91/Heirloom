# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Input.GetButton (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Input][1]

### GetButton(Key)

Gets the latest state of a button the keyboard.

```cs
public static ButtonState GetButton(Key key)
```

| Name | Type     | Summary   |
|------|----------|-----------|
| key  | [Key][2] | Some key. |

> **Returns** - [ButtonState][3]

### GetButton(MouseButton)

Gets the latest state of a mouse button.

```cs
public static ButtonState GetButton(MouseButton button)
```

| Name   | Type             | Summary      |
|--------|------------------|--------------|
| button | [MouseButton][4] | Some button. |

> **Returns** - [ButtonState][3]

[0]: ../../../Heirloom.Core.md
[1]: ../Input.md
[2]: ../Key.md
[3]: ../ButtonState.md
[4]: ../MouseButton.md
