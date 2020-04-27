# GraphicsAdapterInfo

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

```cs
public struct GraphicsAdapterInfo
```

--------------------------------------------------------------------------------

**Fields**: [IsMobilePlatform][1], [Vendor][2], [Name][3]

--------------------------------------------------------------------------------

## Constructors

### GraphicsAdapterInfo(bool, string, string)

```cs
GraphicsAdapterInfo(bool isMobilePlatform, string vendor, string name)
```

## Fields

| Name                  | Summary                                                                                                |
|-----------------------|--------------------------------------------------------------------------------------------------------|
| [IsMobilePlatform][1] | Gets a value that determines if this application has been detected to be running on a mobile platform. |
| [Vendor][2]           | The adapter vedor (ie, NVIDIA or AMD).                                                                 |
| [Name][3]             | The adapter name (ie, GTX 1080).                                                                       |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.GraphicsAdapterInfo.IsMobilePlatform.md
[2]: Heirloom.GraphicsAdapterInfo.Vendor.md
[3]: Heirloom.GraphicsAdapterInfo.Name.md