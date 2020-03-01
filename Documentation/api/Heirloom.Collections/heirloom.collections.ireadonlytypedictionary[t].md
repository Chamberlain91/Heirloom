# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../heirloom.collections/heirloom.collections.md)</small>  

## IReadOnlyTypeDictionary\<T> (Interface)
<small>**Namespace**: Heirloom.Collections</sub></small>  
<small>**Interfaces**: IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable</small>  

A read-only view of [ITypeDictionary\<T>](heirloom.collections.itypedictionary[t].md).

| Methods | Summary |
|---------|---------|
| [Contains](#CONFC14FF81) | Does this type dictionary contain this object? |
| [ContainsType<X>](#CON93D1CDF6) | Does the dictionary contain any object that inherits from the specified type. |
| [GetItemsByType<X>](#GETBA3442D3) | Enumerates any object that inherits from the specified type. |

### Methods

#### <a name="CONFC14FF81"></a>Contains(T obj) : bool

<small>`Abstract`</small>

Does this type dictionary contain this object?

<small>**obj**: <param name="obj">Some object.</param>  
</small>

#### <a name="CON93D1CDF6"></a>ContainsType<X>() : bool

<small>`Abstract`</small>

Does the dictionary contain any object that inherits from the specified type.

#### <a name="GETBA3442D3"></a>GetItemsByType<X>() : IEnumerable\<X>

<small>`Abstract`</small>

Enumerates any object that inherits from the specified type.

