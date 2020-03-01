# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## TextLayout (Static Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

Utility to measure text and manually invoke the text layout function.   
 Internally used by `Heirloom.Drawing.Graphics.DrawText(System.String,Heirloom.Math.Rectangle@,Heirloom.Drawing.Font,System.Int32,Heirloom.Drawing.TextAlign,Heirloom.Drawing.DrawTextCallback)` and its variants.

| Methods                       | Summary                                                                                                                         |
|-------------------------------|---------------------------------------------------------------------------------------------------------------------------------|
| [Measure](#MEAF6045C6)        | Computes the bounding box that the specified text will occupy within an infinite layout size.                                   |
| [Measure](#MEADB2ACB9C)       | Computes the bounding box that the specified text will occupy within the given layout size.                                     |
| [Measure](#MEA6FE8E546)       | Computes the bounding box that the specified text will occupy within the given layout size.                                     |
| [PerformLayout](#PEREE2634A7) | Performs the layout of text around the given position with the specified font and size, invoking the callback at each location. |
| [PerformLayout](#PER7C4785FB) | Performs the layout of text within the given bounds with the specified font and size, invoking the callback at each location.   |

### Methods

#### <a name="MEAF6045C6"></a>Measure(string text, [Font](Heirloom.Drawing.Font.md) font, int fontSize) : [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Computes the bounding box that the specified text will occupy within an infinite layout size.

<small>**text**: <param name="text">The text to layout and measure.</param></small>  
<small>**fontSize**: <param name="fontSize">The font size to use.</param></small>  

#### <a name="MEADB2ACB9C"></a>Measure(string text, in [Size](../Heirloom.Math/Heirloom.Math.Size.md) layoutSize, [Font](Heirloom.Drawing.Font.md) font, int fontSize) : [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Computes the bounding box that the specified text will occupy within the given layout size.

<small>**text**: <param name="text">The text to layout and measure.</param></small>  
<small>**layoutSize**: <param name="layoutSize">The size of the layout box.</param></small>  
<small>**fontSize**: <param name="fontSize">The font size to use.</param></small>  

#### <a name="MEA6FE8E546"></a>Measure(string text, in [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md) layoutBox, [Font](Heirloom.Drawing.Font.md) font, int fontSize) : [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Computes the bounding box that the specified text will occupy within the given layout size.

<small>**text**: <param name="text">The text to layout and measure.</param></small>  
<small>**fontSize**: <param name="fontSize">The font size to use.</param></small>  

#### <a name="PEREE2634A7"></a>PerformLayout(string text, [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, [Font](Heirloom.Drawing.Font.md) font, int size, [TextAlign](Heirloom.Drawing.TextAlign.md) align, [TextLayoutCallback](Heirloom.Drawing.TextLayoutCallback.md) layoutCallback) : void
<small>`Static`</small>

Performs the layout of text around the given position with the specified font and size, invoking the callback at each location.


#### <a name="PER7C4785FB"></a>PerformLayout(string text, [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md) bounds, [Font](Heirloom.Drawing.Font.md) font, int size, [TextAlign](Heirloom.Drawing.TextAlign.md) align, [TextLayoutCallback](Heirloom.Drawing.TextLayoutCallback.md) layoutCallback) : void
<small>`Static`</small>

Performs the layout of text within the given bounds with the specified font and size, invoking the callback at each location.


