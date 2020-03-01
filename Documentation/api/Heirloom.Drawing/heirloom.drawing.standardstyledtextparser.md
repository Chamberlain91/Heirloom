# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## StandardStyledTextParser (Abstract Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Inherits**: [StyledTextParser](heirloom.drawing.styledtextparser.md)</small>  

Provides implementation of a BBCode-esque text markup parser.

| Methods | Summary |
|---------|---------|
| [AddKeyword](#ADD706D8521) |  |
| [Parse](#PARA8FC472) | Parse the input text and returns a [StyledText](heirloom.drawing.styledtext.md) object. |

### Constructors

#### StandardStyledTextParser()

### Methods

#### <a name="ADD706D8521"></a>AddKeyword(string keyword, [DrawTextCallback](heirloom.drawing.drawtextcallback.md) callback) : void

<small>`Protected`</small>


#### <a name="PARA8FC472"></a>Parse(string markup) : [StyledText](heirloom.drawing.styledtext.md)

<small>`Virtual`</small>

Parse the input text and returns a [StyledText](heirloom.drawing.styledtext.md) object.


