# UnicodeCharacter

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Represents a single 32 bit Unicode character.

```cs
public struct UnicodeCharacter : IComparable<UnicodeCharacter>, IEquatable<UnicodeCharacter>
```

--------------------------------------------------------------------------------

**Inherits**: IComparable\<UnicodeCharacter>, IEquatable\<UnicodeCharacter>

**Methods**: [CompareTo][1]

--------------------------------------------------------------------------------

## Constructors

### UnicodeCharacter(int)

Initializes a new instance of the [UnicodeCharacter][2] struct.

```cs
public UnicodeCharacter(int codePoint)
```

### UnicodeCharacter(char)

Initializes a new instance of the [UnicodeCharacter][2] struct.

```cs
public UnicodeCharacter(char character)
```

### UnicodeCharacter(char, char)

Initializes a new instance of the [UnicodeCharacter][2] struct.

```cs
public UnicodeCharacter(char highSurrogate, char lowSurrogate)
```

## Methods

| Name           | Summary                                        |
|----------------|------------------------------------------------|
| [CompareTo][1] | Compares this instance to the specified value. |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.UnicodeCharacter.CompareTo.md
[2]: Heirloom.UnicodeCharacter.md
