# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## TypeDictionary\<T> (Sealed Class)
<small>**Namespace**: Heirloom.Collections</sub></small>  
<small>**Interfaces**: [ITypeDictionary\<T>](Heirloom.Collections.ITypeDictionary[T].md), [IReadOnlyTypeDictionary\<T>](Heirloom.Collections.IReadOnlyTypeDictionary[T].md), IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable</small>  

Manages objects by their type hierarchy up to the base type, allowing access and enumeration of objects by type.

| Properties         | Summary |
|--------------------|---------|
| [Count](#COUN73CA) |         |

| Methods                        | Summary |
|--------------------------------|---------|
| [Add](#ADDBCD0)                |         |
| [Remove](#REMOF107)            |         |
| [Contains](#CONTD0AE)          |         |
| [ContainsType<X>](#CONT7AD5)   |         |
| [GetItemsByType<X>](#GETI6B56) |         |
| [GetEnumerator](#GETEF1F9)     |         |

### Constructors

#### TypeDictionary()

### Properties

#### <a name="COUN73CA"></a> Count : int

<small>`Read Only`</small>

### Methods

#### <a name="ADD(9453"></a> Add(T item) : bool
<small>`Virtual`</small>


#### <a name="REMO291D"></a> Remove(T item) : bool
<small>`Virtual`</small>


#### <a name="CONT50B6"></a> Contains(T item) : bool
<small>`Virtual`</small>


#### <a name="CONT93D1"></a> ContainsType<X>() : bool
<small>`Virtual`</small>

#### <a name="GETIBA34"></a> GetItemsByType<X>() : IEnumerable\<X>
<small>`Virtual`, `IteratorStateMachineAttribute`</small>

#### <a name="GETEDDD1"></a> GetEnumerator() : IEnumerator\<T>
<small>`Virtual`</small>

