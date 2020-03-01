# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## StandardStyledTextParser (Abstract Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Inherits**: [StyledTextParser](Heirloom.Drawing.StyledTextParser.md)</small>  

Provides implementation of a BBCode-esque text markup parser.

| Methods                    | Summary                                                                                 |
|----------------------------|-----------------------------------------------------------------------------------------|
| [AddKeyword](#ADDF8DA6D18) |                                                                                         |
| [Parse](#PARB2AE6A55)      | Parse the input text and returns a [StyledText](Heirloom.Drawing.StyledText.md) object. |

### Constructors

#### StandardStyledTextParser()

### Methods

#### <a name="ADDB92DC801"></a>AddKeyword(string keyword, [DrawTextCallback](Heirloom.Drawing.DrawTextCallback.md) callback) : void
<small>`Protected`</small>


#### <a name="PARD4D42BB2"></a>Parse(string markup) : [StyledText](Heirloom.Drawing.StyledText.md)
<small>`Virtual`</small>

Parse the input text and returns a [StyledText](Heirloom.Drawing.StyledText.md) object.


