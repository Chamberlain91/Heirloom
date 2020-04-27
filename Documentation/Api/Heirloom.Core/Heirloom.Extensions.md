# Extensions

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Provides extension methods for `Random` and other related random operations.

```cs
public static class Extensions
```

--------------------------------------------------------------------------------

**Static Methods**: [Apply\<T>][1], [ToHeap\<T>][2], [IsAscendingOrder\<T>][3], [IsDescendingOrder\<T>][4], [Sample][5], [NextFloat][6], [NextDouble][7], [Next][8], [NextVectorDisk][9], [NextUnitVector][10], [NextVector][11], [Chance][12], [Choose\<T>][13], [Shuffle\<T>][14], [ReadAllText][15], [ReadLines][16], [ReadAllBytes][17], [ToIdentifier][18], [Shorten][19], [GetCharacter][20], [ToSnakeCase][21], [ToShoutingCase][22], [ToSmartDisplayName][23]

--------------------------------------------------------------------------------

## Methods

| Name                       | Summary                                                                                                                                                                          |
|----------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Apply\<T>][1]             | Applies a function to each item in the enumerable.                                                                                                                               |
| [ToHeap\<T>][2]            | Constructs a new [Heap\\<T>][24] from an `IEnumerable\<T>`                                                                                                                       |
| [ToHeap\<T>][2]            | Constructs a new [Heap\\<T>][24] from an `IEnumerable\<T>`                                                                                                                       |
| [IsAscendingOrder\<T>][3]  | Checks if the sequence is in ascending order (sequential equivalent items are considered in order).                                                                              |
| [IsDescendingOrder\<T>][4] | Checks if the sequence is in descending order (sequential equivalent items are considered in order).                                                                             |
| [Sample][5]                | Sample one-dimensional octave noise.                                                                                                                                             |
| [Sample][5]                | Sample two-dimensional noise.                                                                                                                                                    |
| [Sample][5]                | Sample two-dimensional octave noise.                                                                                                                                             |
| [Sample][5]                | Sample two-dimensional octave noise.                                                                                                                                             |
| [Sample][5]                | Sample three-dimensional octave noise.                                                                                                                                           |
| [NextFloat][6]             | Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.                                                                                  |
| [NextFloat][6]             | Returns a random floating-point number that is within the specified range.                                                                                                       |
| [NextDouble][7]            | Returns a random floating-point number that is within the specified range.                                                                                                       |
| [NextFloat][6]             | Returns a random floating-point number that is within the specified range.                                                                                                       |
| [Next][8]                  | Returns a random integer number that is within the specified range.                                                                                                              |
| [NextVectorDisk][9]        | Returns a random point within a unit circle.                                                                                                                                     |
| [NextVectorDisk][9]        | Returns a random point within a circle.                                                                                                                                          |
| [NextUnitVector][10]       | Returns a random unit vector (point on edge of unit circle).                                                                                                                     |
| [NextVector][11]           | Returns a random point within the specified rectangular domain.                                                                                                                  |
| [Chance][12]               | Randomly return true for occurrences with the specified probability.                                                                                                             |
| [Choose\<T>][13]           | Randomly select one of the specified items.                                                                                                                                      |
| [Choose\<T>][13]           |                                                                                                                                                                                  |
| [Shuffle\<T>][14]          | Shuffles all elements in the list randomly.                                                                                                                                      |
| [Shuffle\<T>][14]          | Shuffles all elements in the list randomly.                                                                                                                                      |
| [ReadAllText][15]          | Reads the entire contents of the stream as a block of text.                                                                                                                      |
| [ReadLines][16]            | Reads the entire contents of the stream line by line.                                                                                                                            |
| [ReadAllBytes][17]         | Reads the entire contents of the stream as blob of bytes.                                                                                                                        |
| [ToIdentifier][18]         | Converts this string into a standardized "identifier".                                                                                                                           |
| [Shorten][19]              | Shortens a string by removing the center portion and replacing with "..." dependant on the given max length. This ensures the shortened string has maxLength or less characters. |
| [GetCharacter][20]         | Gets the ith unicode character of this string.                                                                                                                                   |
| [ToSnakeCase][21]          | Transforms a variable name like string into sname case (ie, "myExampleString" into "my_example_string").                                                                         |
| [ToShoutingCase][22]       | Transforms a variable name like string into sname case (ie, "myExampleString" into "MY_EXAMPLE_STRING").                                                                         |
| [ToSmartDisplayName][23]   | Transform a variable name like string to an improved display string (akin to Unity's NicifyVariableName). Ie, "myExampleString" becomes "My Example String"                      |

[0]: ../Heirloom.Core.md
[1]: Heirloom.Extensions.Apply[T].md
[2]: Heirloom.Extensions.ToHeap[T].md
[3]: Heirloom.Extensions.IsAscendingOrder[T].md
[4]: Heirloom.Extensions.IsDescendingOrder[T].md
[5]: Heirloom.Extensions.Sample.md
[6]: Heirloom.Extensions.NextFloat.md
[7]: Heirloom.Extensions.NextDouble.md
[8]: Heirloom.Extensions.Next.md
[9]: Heirloom.Extensions.NextVectorDisk.md
[10]: Heirloom.Extensions.NextUnitVector.md
[11]: Heirloom.Extensions.NextVector.md
[12]: Heirloom.Extensions.Chance.md
[13]: Heirloom.Extensions.Choose[T].md
[14]: Heirloom.Extensions.Shuffle[T].md
[15]: Heirloom.Extensions.ReadAllText.md
[16]: Heirloom.Extensions.ReadLines.md
[17]: Heirloom.Extensions.ReadAllBytes.md
[18]: Heirloom.Extensions.ToIdentifier.md
[19]: Heirloom.Extensions.Shorten.md
[20]: Heirloom.Extensions.GetCharacter.md
[21]: Heirloom.Extensions.ToSnakeCase.md
[22]: Heirloom.Extensions.ToShoutingCase.md
[23]: Heirloom.Extensions.ToSmartDisplayName.md
[24]: Heirloom.Heap[T].md
