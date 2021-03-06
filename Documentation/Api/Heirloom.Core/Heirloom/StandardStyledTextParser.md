# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## StandardStyledTextParser (Class)

> **Namespace**: [Heirloom][0]

Provides implementation of a BBCode-esque text markup parser.

```cs
public abstract class StandardStyledTextParser : StyledTextParser
```

### Inherits

[StyledTextParser][1]

### Methods

[DefineTag][2], [Parse][3]

## Methods

#### Instance

| Name                           | Return Type     | Summary                                               |
|--------------------------------|-----------------|-------------------------------------------------------|
| [DefineTag(string, Draw...][2] | `void`          | Defines a new tag with the specified callback.        |
| [Parse(string)][3]             | [StyledText][4] | Parse the input text and returns a StyledText object. |

[0]: ../../Heirloom.Core.md
[1]: StyledTextParser.md
[2]: StandardStyledTextParser/DefineTag.md
[3]: StandardStyledTextParser/Parse.md
[4]: StyledText.md
