# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Extensions (Class)

> **Namespace**: [Heirloom][0]

Provides extension methods for `Random` and other related random operations.

```cs
public static class Extensions
```

`ExtensionAttribute`

### Static Methods

[Apply\<T>][1], [Chance][2], [Choose\<T>][3], [GetCharacter][4], [IsAscendingOrder\<T>][5], [IsDescendingOrder\<T>][6], [Next][7], [NextDouble][8], [NextFloat][9], [NextUnitVector][10], [NextVector][11], [NextVectorDisk][12], [ReadAllBytes][13], [ReadAllText][14], [ReadLines][15], [Sample][16], [Shorten][17], [Shuffle\<T>][18], [ToHeap\<T>][19], [ToIdentifier][20], [ToShoutingCase][21], [ToSmartDisplayName][22], [ToSnakeCase][23]

## Methods

| Name                            | Return Type            | Summary                                                                |
|---------------------------------|------------------------|------------------------------------------------------------------------|
| [Apply<T>(IEnumerable<T...][1]  | `void`                 | Applies a function to each item in the enumerable.                     |
| [Chance(Random, float)][2]      | `bool`                 | Randomly return true for occurrences with the specified probability.   |
| [Choose<T>(Random, IRea...][3]  | `T`                    | Randomly select one of the specified items.                            |
| [Choose<T>(Random, para...][3]  | `T`                    |                                                                        |
| [GetCharacter(string, int)][4]  | [UnicodeCharacter][24] | Gets the ith unicode character of this string.                         |
| [IsAscendingOrder<T>(IE...][5]  | `bool`                 | Checks if the sequence is in ascending order (sequential equivalent... |
| [IsDescendingOrder<T>(I...][6]  | `bool`                 | Checks if the sequence is in descending order (sequential equivalen... |
| [Next(Random, IntRange)][7]     | `float`                | Returns a random integer number that is within the specified range.    |
| [NextDouble(Random, dou...][8]  | `double`               | Returns a random floating-point number that is within the specified... |
| [NextFloat(Random)][9]          | `float`                | Returns a random floating-point number that is greater than or equa... |
| [NextFloat(Random, floa...][9]  | `float`                | Returns a random floating-point number that is within the specified... |
| [NextFloat(Random, Range)][9]   | `float`                | Returns a random floating-point number that is within the specified... |
| [NextUnitVector(Random)][10]    | [Vector][25]           | Returns a random unit vector (point on edge of unit circle).           |
| [NextVector(Random, in ...][11] | [Vector][25]           | Returns a random point within the specified rectangular domain.        |
| [NextVectorDisk(Random)][12]    | [Vector][25]           | Returns a random point within a unit circle.                           |
| [NextVectorDisk(Random,...][12] | [Vector][25]           | Returns a random point within a circle.                                |
| [ReadAllBytes(Stream)][13]      | ` byte[]`              | Reads the entire contents of the stream as blob of bytes.              |
| [ReadAllText(Stream)][14]       | `string`               | Reads the entire contents of the stream as a block of text.            |
| [ReadLines(Stream)][15]         | `IEnumerable\<string>` | Reads the entire contents of the stream line by line.                  |
| [Sample(INoise1D, float...][16] | `float`                | Sample one-dimensional octave noise.                                   |
| [Sample(INoise2D, in Ve...][16] | `float`                | Sample two-dimensional noise.                                          |
| [Sample(INoise2D, in Ve...][16] | `float`                | Sample two-dimensional octave noise.                                   |
| [Sample(INoise2D, float...][16] | `float`                | Sample two-dimensional octave noise.                                   |
| [Sample(INoise3D, float...][16] | `float`                | Sample three-dimensional octave noise.                                 |
| [Shorten(string, int)][17]      | `string`               | Shortens a string by removing the center portion and replacing with... |
| [Shuffle<T>(Random, ILi...][18] | `void`                 | Shuffles all elements in the list randomly.                            |
| [Shuffle<T>(IList<T>, R...][18] | `void`                 | Shuffles all elements in the list randomly.                            |
| [ToHeap<T>(IEnumerable<...][19] | [Heap\<T>][26]         | Constructs a new Heap<T> from an IEnumerable<T>                        |
| [ToHeap<T>(IEnumerable<...][19] | [Heap\<T>][26]         | Constructs a new Heap<T> from an IEnumerable<T>                        |
| [ToIdentifier(string)][20]      | `string`               | Converts this string into a standardized "identifier".                 |
| [ToShoutingCase(string)][21]    | `string`               | Transforms a variable name like string into sname case (ie, "myExam... |
| [ToSmartDisplayName(str...][22] | `string`               | Transform a variable name like string to an improved display string... |
| [ToSnakeCase(string)][23]       | `string`               | Transforms a variable name like string into sname case (ie, "myExam... |

[0]: ../../Heirloom.Core.md
[1]: Extensions/Apply[T].md
[2]: Extensions/Chance.md
[3]: Extensions/Choose[T].md
[4]: Extensions/GetCharacter.md
[5]: Extensions/IsAscendingOrder[T].md
[6]: Extensions/IsDescendingOrder[T].md
[7]: Extensions/Next.md
[8]: Extensions/NextDouble.md
[9]: Extensions/NextFloat.md
[10]: Extensions/NextUnitVector.md
[11]: Extensions/NextVector.md
[12]: Extensions/NextVectorDisk.md
[13]: Extensions/ReadAllBytes.md
[14]: Extensions/ReadAllText.md
[15]: Extensions/ReadLines.md
[16]: Extensions/Sample.md
[17]: Extensions/Shorten.md
[18]: Extensions/Shuffle[T].md
[19]: Extensions/ToHeap[T].md
[20]: Extensions/ToIdentifier.md
[21]: Extensions/ToShoutingCase.md
[22]: Extensions/ToSmartDisplayName.md
[23]: Extensions/ToSnakeCase.md
[24]: UnicodeCharacter.md
[25]: Vector.md
[26]: ../Heirloom.Collections/Heap[T].md
