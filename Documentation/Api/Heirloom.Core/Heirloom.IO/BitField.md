# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## BitField (Struct)

> **Namespace**: [Heirloom.IO][0]

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

[Equals][3], [GetBit][4], [GetEnumerator][5], [GetHashCode][6], [SetBit][7]

## Properties

#### Instance

| Name         | Type   | Summary |
|--------------|--------|---------|
| [Count][1]   | `int`  |         |
| [Indexer][2] | `bool` |         |

## Methods

#### Instance

| Name                   | Return Type         | Summary                               |
|------------------------|---------------------|---------------------------------------|
| [Equals(object)][3]    | `bool`              |                                       |
| [Equals(BitField)][3]  | `bool`              |                                       |
| [GetBit(int)][4]       | `bool`              | Gets the bit value at `index` offset. |
| [GetEnumerator()][5]   | `IEnumerator<bool>` |                                       |
| [GetHashCode()][6]     | `int`               |                                       |
| [SetBit(int, bool)][7] | `void`              | Sets the bit value at `index` offset. |

[0]: ../../Heirloom.Core.md
[1]: BitField/Count.md
[2]: BitField/Indexer.md
[3]: BitField/Equals.md
[4]: BitField/GetBit.md
[5]: BitField/GetEnumerator.md
[6]: BitField/GetHashCode.md
[7]: BitField/SetBit.md
