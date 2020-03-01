# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## TypeDictionary\<T> (Sealed Class)
<small>**Namespace**: Heirloom.Collections</sub></small>  
<small>**Interfaces**: [ITypeDictionary\<T>](Heirloom.Collections.ITypeDictionary[T].md), [IReadOnlyTypeDictionary\<T>](Heirloom.Collections.IReadOnlyTypeDictionary[T].md), IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable</small>  

Manages objects by their type hierarchy up to the base type, allowing access and enumeration of objects by type.

| Properties            | Summary |
|-----------------------|---------|
| [Count](#COU73CA0BBB) |         |

| Methods                           | Summary |
|-----------------------------------|---------|
| [Add](#ADD9453EEA5)               |         |
| [Remove](#REM291D149A)            |         |
| [Contains](#CON50B6A9F)           |         |
| [ContainsType<X>](#CON93D1CDF6)   |         |
| [GetItemsByType<X>](#GETBA3442D3) |         |
| [GetEnumerator](#GETDDD17E2E)     |         |

### Constructors

#### TypeDictionary()

### Properties

#### <a name="COU73CA0BBB"></a>Count : int

<small>`Read Only`</small>

### Methods

#### <a name="ADD9453EEA5"></a>Add(T item) : bool
<small>`Virtual`</small>


#### <a name="REM291D149A"></a>Remove(T item) : bool
<small>`Virtual`</small>


#### <a name="CON50B6A9F"></a>Contains(T item) : bool
<small>`Virtual`</small>


#### <a name="CON93D1CDF6"></a>ContainsType<X>() : bool
<small>`Virtual`</small>

#### <a name="GETBA3442D3"></a>GetItemsByType<X>() : IEnumerable\<X>
<small>`Virtual`, `IteratorStateMachineAttribute`</small>

#### <a name="GETDDD17E2E"></a>GetEnumerator() : IEnumerator\<T>
<small>`Virtual`</small>

