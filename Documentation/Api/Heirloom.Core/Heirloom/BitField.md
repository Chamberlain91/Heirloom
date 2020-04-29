# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## BitField Struct

> **Namespace**: [Heirloom][0]  

A structured byte to configure the 8 individual bits as a method of storing 'compressed' boolean values.

```cs
public struct BitField : IEquatable<BitField>, IReadOnlyList<bool>, IReadOnlyCollection<bool>, IEnumerable<bool>, IEnumerable
```

### Inherits

IEquatable\<BitField>, IReadOnlyList\<bool>, IReadOnlyCollection\<bool>, IEnumerable\<bool>, IEnumerable

#### Properties

[Indexer][1], [Count][2]

#### Methods

[GetBit][3], [SetBit][4], [GetEnumerator][5]

## Properties

| Name         | Summary |
|--------------|---------|
| [Indexer][1] |         |
| [Count][2]   |         |

## Methods

| Name               | Summary                               |
|--------------------|---------------------------------------|
| [GetBit][3]        | Gets the bit value at `index` offset. |
| [SetBit][4]        | Sets the bit value at `index` offset. |
| [GetEnumerator][5] |                                       |

[0]: ../../Heirloom.Core.md
[1]: BitField/Indexer.md
[2]: BitField/Count.md
[3]: BitField/GetBit.md
[4]: BitField/SetBit.md
[5]: BitField/GetEnumerator.md
