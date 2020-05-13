# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## ObjectPool\<T> (Class)

> **Namespace**: [Heirloom.Collections][0]

Implements an object pool to recycle objects and reduce allocatio stress.

```cs
public class ObjectPool<T>
```

### Methods

[CreateItem][1], [Recycle][2], [Request][3], [ResetItem][4]

## Methods

#### Instance

| Name              | Return Type | Summary                                                                |
|-------------------|-------------|------------------------------------------------------------------------|
| [CreateItem()][1] | `T`         | Constructs an instance of `T` .                                        |
| [Recycle(T)][2]   | `void`      | Recycles an object owned by this pool for layer reuse with Request .   |
| [Request()][3]    | `T`         | Requests an object from the ObjectPool<T> .                            |
| [ResetItem(T)][4] | `void`      | When ObjectPool<T> is subclassed, this can be overriden to clear st... |

[0]: ../../Heirloom.Core.md
[1]: ObjectPool[T]/CreateItem.md
[2]: ObjectPool[T]/Recycle.md
[3]: ObjectPool[T]/Request.md
[4]: ObjectPool[T]/ResetItem.md
