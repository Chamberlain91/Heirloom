# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GameLoop.Create (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [GameLoop][1]

### Create(GraphicsContext, UpdateFunction, int)

Creates a render loop instance from the given context and method reference.

```cs
public static GameLoop Create(GraphicsContext gfx, UpdateFunction update, int frameRate = -1)
```

| Name      | Type                 | Summary                                           |
|-----------|----------------------|---------------------------------------------------|
| gfx       | [GraphicsContext][2] | The relevant graphics context.                    |
| update    | [UpdateFunction][3]  | The relevant update function.                     |
| frameRate | `int`                | The desired fixed frame rate or -1 for unlimited. |

> **Returns** - [GameLoop][1]

[0]: ../../../Heirloom.Core.md
[1]: ../GameLoop.md
[2]: ../GraphicsContext.md
[3]: ../UpdateFunction.md
