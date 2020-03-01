# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## FreeList\<T> (Sealed Class)
<small>**Namespace**: Heirloom.Collections</sub></small>  

A free list an allocation-centric data structure that allows insertion and removal of elements in O(1) time, but does not behave like a typical "list" data type.

| Properties            | Summary                                                                                                           |
|-----------------------|-------------------------------------------------------------------------------------------------------------------|
| [Item](#ITEM8B5A)     |                                                                                                                   |
| [Capacity](#CAPA30F4) | Gets the total number of elements that can be stored in this [FreeList\<T>](Heirloom.Collections.FreeList[T].md). |
| [Count](#COUN73CA)    | Gets the number of elements stored in this [FreeList\<T>](Heirloom.Collections.FreeList[T].md).                   |

| Methods             | Summary                                                                                                                                                           |
|---------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Clear](#CLEA3BB2)  | Clears the free list, invalidating all indices and clearing element data.                                                                                         |
| [Insert](#INSEC7B1) | Inserts an element into the free list and returns its index.                                                                                                      |
| [Remove](#REMOF107) | Removes an element from the free list by an index returned by `Heirloom.Collections.FreeList`1.Insert(`0)`. This index is not validated, you must be responsible. |
| [Resize](#RESIFD0A) | Resize the free list with an increased capacity.                                                                                                                  |

### Constructors

#### FreeList(int capacity)

Constructs a new free list instance.

### Properties

#### <a name="ITEM8B5A"></a> Item : T

<small>`Read Only`</small>

#### <a name="CAPA30F4"></a> Capacity : int

<small>`Read Only`</small>

Gets the total number of elements that can be stored in this [FreeList\<T>](Heirloom.Collections.FreeList[T].md).

#### <a name="COUN73CA"></a> Count : int

<small>`Read Only`</small>

Gets the number of elements stored in this [FreeList\<T>](Heirloom.Collections.FreeList[T].md).

### Methods

#### <a name="CLEA4538"></a> Clear() : void

Clears the free list, invalidating all indices and clearing element data.

#### <a name="INSE5BA4"></a> Insert(T value) : int

Inserts an element into the free list and returns its index.


#### <a name="REMO37A8"></a> Remove(int index) : void

Removes an element from the free list by an index returned by `Heirloom.Collections.FreeList`1.Insert(`0)`.   
 This index is not validated, you must be responsible.


#### <a name="RESI3BF6"></a> Resize(int newCapacity) : void

Resize the free list with an increased capacity.


