# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Extensions.Shorten (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Extensions][1]

### Shorten(string, int)

Shortens a string by removing the center portion and replacing with "..." dependant on the given max length. This ensures the shortened string has maxLength or less characters.

```cs
public static string Shorten(string this, int maxLength = 15)
```

`ExtensionAttribute`

| Name      | Type     | Summary |
|-----------|----------|---------|
| this      | `string` |         |
| maxLength | `int`    |         |

> **Returns** - `string`

[0]: ../../../Heirloom.Core.md
[1]: ../Extensions.md
