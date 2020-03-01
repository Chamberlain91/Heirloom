# Heirloom.IO

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## BitField (Struct)
<small>**Namespace**: Heirloom.IO</sub></small>  
<small>**Interfaces**: IEquatable\<BitField>, IReadOnlyList\<bool>, IReadOnlyCollection\<bool>, IEnumerable\<bool>, IEnumerable</small>  

A structured byte to configure the 8 individual bits as a method of storing 'compressed' boolean values.

| Properties            | Summary |
|-----------------------|---------|
| [Item](#ITE8B5A2F95)  |         |
| [Count](#COU73CA0BBB) |         |

| Methods                       | Summary                               |
|-------------------------------|---------------------------------------|
| [GetBit](#GETE7C6484D)        | Gets the bit value at `index` offset. |
| [SetBit](#SETE7C69839)        | Sets the bit value at `index` offset. |
| [GetEnumerator](#GETF1F90828) |                                       |

### Constructors

#### BitField(byte value = 0)

### Properties

#### <a name="ITE8B5A2F95"></a>Item : bool


#### <a name="COU73CA0BBB"></a>Count : int

<small>`Read Only`</small>

### Methods

#### <a name="GETADBE54DD"></a>GetBit(int index) : bool

Gets the bit value at `index` offset.


#### <a name="SET69B76502"></a>SetBit(int index, bool bit) : void

Sets the bit value at `index` offset.


#### <a name="GETE8195C76"></a>GetEnumerator() : IEnumerator\<bool>
<small>`Virtual`</small>

