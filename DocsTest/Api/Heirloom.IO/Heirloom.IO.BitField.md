# Heirloom.IO

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## BitField (Struct)
<small>**Namespace**: Heirloom.IO</sub></small>  
<small>**Interfaces**: IEquatable\<BitField>, IReadOnlyList\<bool>, IReadOnlyCollection\<bool>, IEnumerable\<bool>, IEnumerable</small>  

A structured byte to configure the 8 individual bits as a method of storing 'compressed' boolean values.

| Properties         | Summary |
|--------------------|---------|
| [Item](#ITEM8B5A)  |         |
| [Count](#COUN73CA) |         |

| Methods                    | Summary                               |
|----------------------------|---------------------------------------|
| [GetBit](#GETBE7C6)        | Gets the bit value at `index` offset. |
| [SetBit](#SETBE7C6)        | Sets the bit value at `index` offset. |
| [GetEnumerator](#GETEF1F9) |                                       |

### Constructors

#### BitField(byte value = 0)

### Properties

#### <a name="ITEM8B5A"></a> Item : bool


#### <a name="COUN73CA"></a> Count : int

<small>`Read Only`</small>

### Methods

#### <a name="GETBADBE"></a> GetBit(int index) : bool

Gets the bit value at `index` offset.


#### <a name="SETB69B7"></a> SetBit(int index, bool bit) : void

Sets the bit value at `index` offset.


#### <a name="GETEE819"></a> GetEnumerator() : IEnumerator\<bool>
<small>`Virtual`</small>

