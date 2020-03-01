# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## StringExtensions (Static Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

| Methods                         | Summary                                                                                                                                                                          |
|---------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Shorten](#SHORD495)            | Shortens a string by removing the center portion and replacing with "..." dependant on the given max length. This ensures the shortened string has maxLength or less characters. |
| [GetCharacter](#GETC64B8)       |                                                                                                                                                                                  |
| [ToSnakeCase](#TOSNABE0)        | Transforms a variable name like string into sname case (ie, "myExampleString" into "my_example_string").                                                                         |
| [ToShoutingCase](#TOSHF298)     | Transforms a variable name like string into sname case (ie, "myExampleString" into "MY_EXAMPLE_STRING").                                                                         |
| [ToSmartDisplayName](#TOSMBAF9) | Transform a variable name like string to an improved display string (akin to Unity's NicifyVariableName). Ie, "myExampleString" becomes "My Example String"                      |

### Methods

#### <a name="SHORA525"></a> Shorten(string this, int maxLength = 15) : string
<small>`Static`, `ExtensionAttribute`</small>

Shortens a string by removing the center portion and replacing with "..." dependant on the given max length. This ensures the shortened string has maxLength or less characters.

<small>**this**: <param name="this"></param></small>  
<small>**maxLength**: <param name="maxLength"></param></small>  

#### <a name="GETCD5A9"></a> GetCharacter(string text, int i) : [UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md)
<small>`Static`, `ExtensionAttribute`</small>


#### <a name="TOSNE961"></a> ToSnakeCase(string this) : string
<small>`Static`, `ExtensionAttribute`</small>

Transforms a variable name like string into sname case (ie, "myExampleString" into "my_example_string").


#### <a name="TOSHBEC3"></a> ToShoutingCase(string this) : string
<small>`Static`, `ExtensionAttribute`</small>

Transforms a variable name like string into sname case (ie, "myExampleString" into "MY_EXAMPLE_STRING").


#### <a name="TOSM27D9"></a> ToSmartDisplayName(string this) : string
<small>`Static`, `ExtensionAttribute`</small>

Transform a variable name like string to an improved display string (akin to Unity's NicifyVariableName).   
 Ie, "myExampleString" becomes "My Example String"


