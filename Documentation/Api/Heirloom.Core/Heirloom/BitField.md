# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## BitField (Struct)

> **Namespace**: [Heirloom][0]

A structured byte to configure the 8 individual bits as a method of storing 'compressed' boolean values.

```cs
public struct BitField : IEquatable<BitField>, IReadOnlyList<bool>, IReadOnlyCollection<bool>, IEnumerable<bool>, IEnumerable
```

`SerializableAttribute`

### Inherits

IEquatable\<BitField>, IReadOnlyList\<bool>, IReadOnlyCollection\<bool>, IEnumerable\<bool>, IEnumerable

### Properties

[Count][1], [Indexer][2]

### Methods

[GetBit][3], [GetEnumerator][4], [SetBit][5]

## Properties

#### Instance

| Name         | Type   | Summary |
|--------------|--------|---------|
| [Count][1]   | `int`  |         |
| [Indexer][2] | `bool` |         |

## Methods

#### Instance

| Name                   | Return Type          | Summary                               |
|------------------------|----------------------|---------------------------------------|
| [GetBit(int)][3]       | `bool`               | Gets the bit value at `index` offset. |
| [GetEnumerator()][4]   | `IEnumerator\<bool>` |                                       |
| [SetBit(int, bool)][5] | `void`               | Sets the bit value at `index` offset. |

[0]: ../../Heirloom.Core.md
[1]: BitField/Count.md
[2]: BitField/Indexer.md
[3]: BitField/GetBit.md
[4]: BitField/GetEnumerator.md
[5]: BitField/SetBit.md
