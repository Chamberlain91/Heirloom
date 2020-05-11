# Heirloom.Desktop

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Desktop][0]

## Application.Run (Method)

> **Namespace**: [Heirloom.Desktop][0]  
> **Declaring Type**: [Application][1]

### Run(Action)

Initializes windowing utilities, executes `startup` and then continuously processes window events until all windows are closed. This is a blocking function.

```cs
public static void Run(Action startup)
```

| Name    | Type     | Summary |
|---------|----------|---------|
| startup | `Action` |         |

> **Returns** - `void`

[0]: ../../../Heirloom.Desktop.md
[1]: ../Application.md
