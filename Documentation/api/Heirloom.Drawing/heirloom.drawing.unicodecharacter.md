# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## UnicodeCharacter (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IComparable\<UnicodeCharacter>, IEquatable\<UnicodeCharacter></small>  

Represents a single 32 bit Unicode character.

| Methods | Summary |
|---------|---------|
| [CompareTo](#COM724F165B) | Compares this instance to the specified value. |

### Constructors

#### UnicodeCharacter(int codePoint)

Initializes a new instance of the [UnicodeCharacter](heirloom.drawing.unicodecharacter.md) struct.

#### UnicodeCharacter(char character)

Initializes a new instance of the [UnicodeCharacter](heirloom.drawing.unicodecharacter.md) struct.

#### UnicodeCharacter(char highSurrogate, char lowSurrogate)

Initializes a new instance of the [UnicodeCharacter](heirloom.drawing.unicodecharacter.md) struct.

### Methods

#### <a name="COM724F165B"></a>CompareTo([UnicodeCharacter](heirloom.drawing.unicodecharacter.md) other) : int

<small>`Virtual`</small>

Compares this instance to the specified value.

<small>**other**: <param name="other">The value to compare.</param>  
</small>

