# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## ITypeDictionary\<T> (Interface)
<small>**Namespace**: Heirloom.Collections</sub></small>  
<small>**Interfaces**: [IReadOnlyTypeDictionary\<T>](Heirloom.Collections.IReadOnlyTypeDictionary[T].md), IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable</small>  

Manages objects by their type hierarchy up to the base type, allowing access by enumeration of objects by type.

| Methods                | Summary                                    |
|------------------------|--------------------------------------------|
| [Add](#ADDBCD0F225)    | Add a new object to the type dictionary.   |
| [Remove](#REMF10744DE) | Remove an object from the type dictionary. |

### Methods

#### <a name="ADD882735FB"></a>Add(T obj) : bool
<small>`Abstract`</small>

Add a new object to the type dictionary.

<small>**obj**: <param name="obj">Some object.</param></small>  

#### <a name="REMA46A9FF0"></a>Remove(T obj) : bool
<small>`Abstract`</small>

Remove an object from the type dictionary.

<small>**obj**: <param name="obj">Some object.</param></small>  

