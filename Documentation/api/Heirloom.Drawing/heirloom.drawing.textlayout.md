# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## TextLayout (Static Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

Utility to measure text and manually invoke the text layout function.   
 Internally used by `Heirloom.Drawing.Graphics.DrawText(System.String,Heirloom.Math.Rectangle@,Heirloom.Drawing.Font,System.Int32,Heirloom.Drawing.TextAlign,Heirloom.Drawing.DrawTextCallback)` and its variants.

| Methods | Summary |
|---------|---------|
| [Measure](#MEA194E2946) | Computes the bounding box that the specified text will occupy within an infinite layout size. |
| [Measure](#MEAF3564EFC) | Computes the bounding box that the specified text will occupy within the given layout size. |
| [Measure](#MEA62691FE6) | Computes the bounding box that the specified text will occupy within the given layout size. |
| [PerformLayout](#PER43915887) | Performs the layout of text around the given position with the specified font and size, invoking the callback at each location. |
| [PerformLayout](#PER94CAB25B) | Performs the layout of text within the given bounds with the specified font and size, invoking the callback at each location. |

### Methods

#### <a name="MEA194E2946"></a>Measure(string text, [Font](heirloom.drawing.font.md) font, int fontSize) : [Rectangle](../heirloom.math/heirloom.math.rectangle.md)

<small>`Static`</small>

Computes the bounding box that the specified text will occupy within an infinite layout size.

<small>**text**: <param name="text">The text to layout and measure.</param>  
</small>
<small>**fontSize**: <param name="fontSize">The font size to use.</param>  
</small>

#### <a name="MEAF3564EFC"></a>Measure(string text, in [Size](../heirloom.math/heirloom.math.size.md) layoutSize, [Font](heirloom.drawing.font.md) font, int fontSize) : [Rectangle](../heirloom.math/heirloom.math.rectangle.md)

<small>`Static`</small>

Computes the bounding box that the specified text will occupy within the given layout size.

<small>**text**: <param name="text">The text to layout and measure.</param>  
</small>
<small>**layoutSize**: <param name="layoutSize">The size of the layout box.</param>  
</small>
<small>**fontSize**: <param name="fontSize">The font size to use.</param>  
</small>

#### <a name="MEA62691FE6"></a>Measure(string text, in [Rectangle](../heirloom.math/heirloom.math.rectangle.md) layoutBox, [Font](heirloom.drawing.font.md) font, int fontSize) : [Rectangle](../heirloom.math/heirloom.math.rectangle.md)

<small>`Static`</small>

Computes the bounding box that the specified text will occupy within the given layout size.

<small>**text**: <param name="text">The text to layout and measure.</param>  
</small>
<small>**fontSize**: <param name="fontSize">The font size to use.</param>  
</small>

#### <a name="PER43915887"></a>PerformLayout(string text, [Vector](../heirloom.math/heirloom.math.vector.md) position, [Font](heirloom.drawing.font.md) font, int size, [TextAlign](heirloom.drawing.textalign.md) align, [TextLayoutCallback](heirloom.drawing.textlayoutcallback.md) layoutCallback) : void

<small>`Static`</small>

Performs the layout of text around the given position with the specified font and size, invoking the callback at each location.


#### <a name="PER94CAB25B"></a>PerformLayout(string text, [Rectangle](../heirloom.math/heirloom.math.rectangle.md) bounds, [Font](heirloom.drawing.font.md) font, int size, [TextAlign](heirloom.drawing.textalign.md) align, [TextLayoutCallback](heirloom.drawing.textlayoutcallback.md) layoutCallback) : void

<small>`Static`</small>

Performs the layout of text within the given bounds with the specified font and size, invoking the callback at each location.


