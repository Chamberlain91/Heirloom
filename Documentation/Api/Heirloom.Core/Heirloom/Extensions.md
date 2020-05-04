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

[Apply\<T>][1], [Chance][2], [Choose\<T>][3], [GetCharacter][4], [IsAscendingOrder\<T>][5], [IsDescendingOrder\<T>][6], [Next][7], [NextDouble][8], [NextFloat][9], [NextUnitVector][10], [NextVector][11], [NextVectorDisk][12], [Randomize\<T>][13], [ReadAllBytes][14], [ReadAllText][15], [ReadLines][16], [Sample][17], [Shorten][18], [Shuffle\<T>][19], [ToHeap\<T>][20], [ToIdentifier][21], [ToShoutingCase][22], [ToSmartDisplayName][23], [ToSnakeCase][24]

## Methods

| Name                            | Return Type            | Summary                                                                |
|---------------------------------|------------------------|------------------------------------------------------------------------|
| [Apply<T>(IEnumerable<T...][1]  | `void`                 | Applies a function to each item in the enumerable.                     |
| [Chance(Random, float)][2]      | `bool`                 | Randomly return true for occurrences with the specified probability.   |
| [Choose<T>(Random, IRea...][3]  | `T`                    | Randomly select one of the specified items.                            |
| [Choose<T>(Random, para...][3]  | `T`                    |                                                                        |
| [GetCharacter(string, int)][4]  | [UnicodeCharacter][25] | Gets the ith unicode character of this string.                         |
| [IsAscendingOrder<T>(IE...][5]  | `bool`                 | Checks if the sequence is in ascending order (sequential equivalent... |
| [IsDescendingOrder<T>(I...][6]  | `bool`                 | Checks if the sequence is in descending order (sequential equivalen... |
| [Next(Random, IntRange)][7]     | `float`                | Returns a random integer number that is within the specified range.    |
| [NextDouble(Random, dou...][8]  | `double`               | Returns a random floating-point number that is within the specified... |
| [NextFloat(Random)][9]          | `float`                | Returns a random floating-point number that is greater than or equa... |
| [NextFloat(Random, floa...][9]  | `float`                | Returns a random floating-point number that is within the specified... |
| [NextFloat(Random, Range)][9]   | `float`                | Returns a random floating-point number that is within the specified... |
| [NextUnitVector(Random)][10]    | [Vector][26]           | Returns a random unit vector (point on edge of unit circle).           |
| [NextVector(Random, in ...][11] | [Vector][26]           | Returns a random point within the specified rectangular domain.        |
| [NextVectorDisk(Random)][12]    | [Vector][26]           | Returns a random point within a unit circle.                           |
| [NextVectorDisk(Random,...][12] | [Vector][26]           | Returns a random point within a circle.                                |
| [Randomize<T>(IList<T>)][13]    | `void`                 |                                                                        |
| [Randomize<T>(IList<T>,...][13] | `void`                 |                                                                        |
| [ReadAllBytes(Stream)][14]      | ` byte[]`              | Reads the entire contents of the stream as blob of bytes.              |
| [ReadAllText(Stream)][15]       | `string`               | Reads the entire contents of the stream as a block of text.            |
| [ReadLines(Stream)][16]         | `IEnumerable\<string>` | Reads the entire contents of the stream line by line.                  |
| [Sample(INoise1D, float...][17] | `float`                | Sample one-dimensional octave noise.                                   |
| [Sample(INoise2D, in Ve...][17] | `float`                | Sample two-dimensional noise.                                          |
| [Sample(INoise2D, in Ve...][17] | `float`                | Sample two-dimensional octave noise.                                   |
| [Sample(INoise2D, float...][17] | `float`                | Sample two-dimensional octave noise.                                   |
| [Sample(INoise3D, float...][17] | `float`                | Sample three-dimensional octave noise.                                 |
| [Shorten(string, int)][18]      | `string`               | Shortens a string by removing the center portion and replacing with... |
| [Shuffle<T>(Random, ILi...][19] | `void`                 | Shuffles all elements in the list randomly.                            |
| [Shuffle<T>(IList<T>, R...][19] | `void`                 | Shuffles all elements in the list randomly.                            |
| [ToHeap<T>(IEnumerable<...][20] | [Heap\<T>][27]         | Constructs a new Heap<T> from an IEnumerable<T>                        |
| [ToHeap<T>(IEnumerable<...][20] | [Heap\<T>][27]         | Constructs a new Heap<T> from an IEnumerable<T>                        |
| [ToIdentifier(string)][21]      | `string`               | Converts this string into a standardized "identifier".                 |
| [ToShoutingCase(string)][22]    | `string`               | Transforms a variable name like string into sname case (ie, "myExam... |
| [ToSmartDisplayName(str...][23] | `string`               | Transform a variable name like string to an improved display string... |
| [ToSnakeCase(string)][24]       | `string`               | Transforms a variable name like string into sname case (ie, "myExam... |

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
[13]: Extensions/Randomize[T].md
[14]: Extensions/ReadAllBytes.md
[15]: Extensions/ReadAllText.md
[16]: Extensions/ReadLines.md
[17]: Extensions/Sample.md
[18]: Extensions/Shorten.md
[19]: Extensions/Shuffle[T].md
[20]: Extensions/ToHeap[T].md
[21]: Extensions/ToIdentifier.md
[22]: Extensions/ToShoutingCase.md
[23]: Extensions/ToSmartDisplayName.md
[24]: Extensions/ToSnakeCase.md
[25]: UnicodeCharacter.md
[26]: Vector.md
[27]: ../Heirloom.Collections/Heap[T].md
