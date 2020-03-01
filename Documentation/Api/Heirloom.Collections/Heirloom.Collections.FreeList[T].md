# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## FreeList\<T> (Sealed Class)
<small>**Namespace**: Heirloom.Collections</sub></small>  
<small>`DefaultMemberAttribute`</small>

A free list an allocation-centric data structure that allows insertion and removal of elements in O(1) time, but does not behave like a typical "list" data type.

| Properties               | Summary                                                                                                           |
|--------------------------|-------------------------------------------------------------------------------------------------------------------|
| [Item](#ITE8B5A2F95)     |                                                                                                                   |
| [Capacity](#CAP30F47D6A) | Gets the total number of elements that can be stored in this [FreeList\<T>](Heirloom.Collections.FreeList[T].md). |
| [Count](#COU73CA0BBB)    | Gets the number of elements stored in this [FreeList\<T>](Heirloom.Collections.FreeList[T].md).                   |

| Methods                | Summary                                                                                                                                                           |
|------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Clear](#CLE4538C554)  | Clears the free list, invalidating all indices and clearing element data.                                                                                         |
| [Insert](#INS5BA4EBFA) | Inserts an element into the free list and returns its index.                                                                                                      |
| [Remove](#REM37A8443A) | Removes an element from the free list by an index returned by `Heirloom.Collections.FreeList`1.Insert(`0)`. This index is not validated, you must be responsible. |
| [Resize](#RES3BF62E34) | Resize the free list with an increased capacity.                                                                                                                  |

### Constructors

#### FreeList(int capacity)

Constructs a new free list instance.

### Properties

#### <a name="ITE8B5A2F95"></a>Item : T

<small>`Read Only`</small>

#### <a name="CAP30F47D6A"></a>Capacity : int

<small>`Read Only`</small>

Gets the total number of elements that can be stored in this [FreeList\<T>](Heirloom.Collections.FreeList[T].md).

#### <a name="COU73CA0BBB"></a>Count : int

<small>`Read Only`</small>

Gets the number of elements stored in this [FreeList\<T>](Heirloom.Collections.FreeList[T].md).

### Methods

#### <a name="CLE4538C554"></a>Clear() : void

Clears the free list, invalidating all indices and clearing element data.

#### <a name="INS5BA4EBFA"></a>Insert(T value) : int

Inserts an element into the free list and returns its index.


#### <a name="REM37A8443A"></a>Remove(int index) : void

Removes an element from the free list by an index returned by `Heirloom.Collections.FreeList`1.Insert(`0)`.   
 This index is not validated, you must be responsible.


#### <a name="RES3BF62E34"></a>Resize(int newCapacity) : void

Resize the free list with an increased capacity.


