# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## StandardStyledTextParser (Abstract Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Inherits**: [StyledTextParser](Heirloom.Drawing.StyledTextParser.md)</small>  

Provides implementation of a BBCode-esque text markup parser.

| Methods                 | Summary                                                                                 |
|-------------------------|-----------------------------------------------------------------------------------------|
| [AddKeyword](#ADDKF8DA) |                                                                                         |
| [Parse](#PARSB2AE)      | Parse the input text and returns a [StyledText](Heirloom.Drawing.StyledText.md) object. |

### Constructors

#### StandardStyledTextParser()

### Methods

#### <a name="ADDKB92D"></a> AddKeyword(string keyword, [DrawTextCallback](Heirloom.Drawing.DrawTextCallback.md) callback) : void
<small>`Protected`</small>


#### <a name="PARSD4D4"></a> Parse(string markup) : [StyledText](Heirloom.Drawing.StyledText.md)
<small>`Virtual`</small>

Parse the input text and returns a [StyledText](Heirloom.Drawing.StyledText.md) object.


