# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## StringExtensions (Static Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

| Methods                            | Summary                                                                                                                                                                          |
|------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Shorten](#SHOD4953605)            | Shortens a string by removing the center portion and replacing with "..." dependant on the given max length. This ensures the shortened string has maxLength or less characters. |
| [GetCharacter](#GET64B8783)        |                                                                                                                                                                                  |
| [ToSnakeCase](#TOSABE0D4FD)        | Transforms a variable name like string into sname case (ie, "myExampleString" into "my_example_string").                                                                         |
| [ToShoutingCase](#TOSF2987B58)     | Transforms a variable name like string into sname case (ie, "myExampleString" into "MY_EXAMPLE_STRING").                                                                         |
| [ToSmartDisplayName](#TOSBAF99BAB) | Transform a variable name like string to an improved display string (akin to Unity's NicifyVariableName). Ie, "myExampleString" becomes "My Example String"                      |

### Methods

#### <a name="SHOA525DB18"></a>Shorten(string this, int maxLength = 15) : string
<small>`Static`, `ExtensionAttribute`</small>

Shortens a string by removing the center portion and replacing with "..." dependant on the given max length. This ensures the shortened string has maxLength or less characters.

<small>**this**: <param name="this"></param></small>  
<small>**maxLength**: <param name="maxLength"></param></small>  

#### <a name="GETD5A92C9D"></a>GetCharacter(string text, int i) : [UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md)
<small>`Static`, `ExtensionAttribute`</small>


#### <a name="TOSE9613E68"></a>ToSnakeCase(string this) : string
<small>`Static`, `ExtensionAttribute`</small>

Transforms a variable name like string into sname case (ie, "myExampleString" into "my_example_string").


#### <a name="TOSBEC33733"></a>ToShoutingCase(string this) : string
<small>`Static`, `ExtensionAttribute`</small>

Transforms a variable name like string into sname case (ie, "myExampleString" into "MY_EXAMPLE_STRING").


#### <a name="TOS27D9F010"></a>ToSmartDisplayName(string this) : string
<small>`Static`, `ExtensionAttribute`</small>

Transform a variable name like string to an improved display string (akin to Unity's NicifyVariableName).   
 Ie, "myExampleString" becomes "My Example String"


