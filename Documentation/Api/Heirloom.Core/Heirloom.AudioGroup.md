# AudioGroup

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

An [AudioNode][1] to mix and apply effects to a group of other nodes.

```cs
public class AudioGroup : AudioNode
```

--------------------------------------------------------------------------------

**Inherits**: [AudioNode][1]

**Properties**: [Parent][2]

**Methods**: [PopulateBuffer][3]

**Static Properties**: [Default][4]

--------------------------------------------------------------------------------

## Constructors

### AudioGroup()

Construct a new audio node that is connected to default audio group (ie, [Default][4] ).

```cs
public AudioGroup()
```

### AudioGroup(AudioGroup)

Construct a new audio group that is connected to the specified parent group.

```cs
public AudioGroup(AudioGroup parentGroup)
```

### AudioGroup(AudioGroup, bool)

```cs
AudioGroup(AudioGroup parentGroup, bool allowOrphan)
```

## Properties

| Name         | Summary                                                           |
|--------------|-------------------------------------------------------------------|
| [Parent][2]  | Gets or sets the parent audio group.                              |
| [Default][4] | Gets the default audio group (ie, the speakers, headphones, etc). |

## Methods

| Name                | Summary |
|---------------------|---------|
| [PopulateBuffer][3] |         |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.AudioNode.md
[2]: Heirloom.AudioGroup.Parent.md
[3]: Heirloom.AudioGroup.PopulateBuffer.md
[4]: Heirloom.AudioGroup.Default.md
