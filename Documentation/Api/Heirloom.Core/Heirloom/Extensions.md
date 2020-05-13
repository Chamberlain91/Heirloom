# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Extensions (Class)

> **Namespace**: [Heirloom][0]

Provides extension methods various types within Heirloom.

```cs
public static class Extensions
```

`ExtensionAttribute`

### Static Methods

[Apply\<T>][1], [Chance][2], [Choose\<T>][3], [GetCharacter][4], [IsAscendingOrder\<T>][5], [IsDescendingOrder\<T>][6], [Next][7], [NextColor][8], [NextColorHue][9], [NextDouble][10], [NextFloat][11], [NextUnitVector][12], [NextVector][13], [NextVectorDisk][14], [Randomize\<T>][15], [ReadAllBytes][16], [ReadAllText][17], [ReadLines][18], [Sample][19], [Shorten][20], [Shuffle\<T>][21], [ToHeap\<T>][22], [ToIdentifier][23], [ToShoutingCase][24], [ToSmartDisplayName][25], [ToSnakeCase][26]

## Methods

| Name                            | Return Type            | Summary                                                                |
|---------------------------------|------------------------|------------------------------------------------------------------------|
| [Apply<T>(IEnumerable<T...][1]  | `void`                 | Applies a function to each item in the enumerable.                     |
| [Chance(Random, float)][2]      | `bool`                 | Randomly return true for occurrences with the specified probability.   |
| [Choose<T>(Random, IRea...][3]  | `T`                    | Randomly select one of the specified items.                            |
| [Choose<T>(Random, para...][3]  | `T`                    |                                                                        |
| [GetCharacter(string, int)][4]  | [UnicodeCharacter][27] | Gets the ith unicode character of this string.                         |
| [IsAscendingOrder<T>(IE...][5]  | `bool`                 | Checks if the sequence is in ascending order (sequential equivalent... |
| [IsDescendingOrder<T>(I...][6]  | `bool`                 | Checks if the sequence is in descending order (sequential equivalen... |
| [Next(Random, IntRange)][7]     | `float`                | Returns a random integer number that is within the specified range.    |
| [NextColor(Random, bool)][8]    | [Color][28]            | Returns a random RGB color (optionally RGBA).                          |
| [NextColorHue(Random, f...][9]  | [Color][28]            | Returns a color with random hue.                                       |
| [NextDouble(Random, dou...][10] | `double`               | Returns a random floating-point number that is within the specified... |
| [NextFloat(Random)][11]         | `float`                | Returns a random floating-point number that is greater than or equa... |
| [NextFloat(Random, floa...][11] | `float`                | Returns a random floating-point number that is within the specified... |
| [NextFloat(Random, Range)][11]  | `float`                | Returns a random floating-point number that is within the specified... |
| [NextUnitVector(Random)][12]    | [Vector][29]           | Returns a random unit vector (point on edge of unit circle).           |
| [NextVector(Random, in ...][13] | [Vector][29]           | Returns a random point within the specified rectangular domain.        |
| [NextVectorDisk(Random)][14]    | [Vector][29]           | Returns a random point within a unit circle.                           |
| [NextVectorDisk(Random,...][14] | [Vector][29]           | Returns a random point within a circle.                                |
| [Randomize<T>(IList<T>)][15]    | `void`                 | Scrambles the items in a list into a randomized order.                 |
| [Randomize<T>(IList<T>,...][15] | `void`                 | Scrambles the items in a list into a randomized order.                 |
| [ReadAllBytes(Stream)][16]      | ` byte[]`              | Reads the entire contents of the stream as blob of bytes.              |
| [ReadAllText(Stream)][17]       | `string`               | Reads the entire contents of the stream as a block of text.            |
| [ReadLines(Stream)][18]         | `IEnumerable<string>`  | Reads the entire contents of the stream line by line.                  |
| [Sample(INoise1D, float...][19] | `float`                | Provides extension methods various types within Heirloom.              |
| [Sample(INoise2D, in Ve...][19] | `float`                | Sample two-dimensional noise.                                          |
| [Sample(INoise2D, in Ve...][19] | `float`                | Sample two-dimensional octave noise.                                   |
| [Sample(INoise2D, float...][19] | `float`                | Sample two-dimensional octave noise.                                   |
| [Sample(INoise3D, float...][19] | `float`                | Sample three-dimensional octave noise.                                 |
| [Shorten(string, int)][20]      | `string`               | Shortens a string by removing the center portion and replacing with... |
| [Shuffle<T>(Random, ILi...][21] | `void`                 | Shuffles all elements in the list randomly.                            |
| [Shuffle<T>(IList<T>, R...][21] | `void`                 | Shuffles all elements in the list randomly.                            |
| [ToHeap<T>(IEnumerable<...][22] | [Heap\<T>][30]         | Constructs a new Heap<T> from an IEnumerable<T>                        |
| [ToHeap<T>(IEnumerable<...][22] | [Heap\<T>][30]         | Constructs a new Heap<T> from an IEnumerable<T>                        |
| [ToIdentifier(string)][23]      | `string`               | Converts this string into a standardized "identifier".                 |
| [ToShoutingCase(string)][24]    | `string`               | Transforms a variable name like string into sname case (ie, "myExam... |
| [ToSmartDisplayName(str...][25] | `string`               | Transform a variable name like string to an improved display string... |
| [ToSnakeCase(string)][26]       | `string`               | Transforms a variable name like string into sname case (ie, "myExam... |

[0]: ../../Heirloom.Core.md
[1]: Extensions/Apply[T].md
[2]: Extensions/Chance.md
[3]: Extensions/Choose[T].md
[4]: Extensions/GetCharacter.md
[5]: Extensions/IsAscendingOrder[T].md
[6]: Extensions/IsDescendingOrder[T].md
[7]: Extensions/Next.md
[8]: Extensions/NextColor.md
[9]: Extensions/NextColorHue.md
[10]: Extensions/NextDouble.md
[11]: Extensions/NextFloat.md
[12]: Extensions/NextUnitVector.md
[13]: Extensions/NextVector.md
[14]: Extensions/NextVectorDisk.md
[15]: Extensions/Randomize[T].md
[16]: Extensions/ReadAllBytes.md
[17]: Extensions/ReadAllText.md
[18]: Extensions/ReadLines.md
[19]: Extensions/Sample.md
[20]: Extensions/Shorten.md
[21]: Extensions/Shuffle[T].md
[22]: Extensions/ToHeap[T].md
[23]: Extensions/ToIdentifier.md
[24]: Extensions/ToShoutingCase.md
[25]: Extensions/ToSmartDisplayName.md
[26]: Extensions/ToSnakeCase.md
[27]: UnicodeCharacter.md
[28]: Color.md
[29]: Vector.md
[30]: ../Heirloom.Collections/Heap[T].md
