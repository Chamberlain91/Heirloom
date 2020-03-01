# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## TextLayoutCallback (Delegate)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Inherits**: MulticastDelegate, Delegate</small>  
<small>**Interfaces**: ICloneable, ISerializable</small>  

### Constructors

#### TextLayoutCallback(object object, IntPtr method)

### Methods

#### <a name="INVFF10D7B7"></a>Invoke(string text, int index, ref [TextLayoutState](heirloom.drawing.textlayoutstate.md) state) : void

<small>`Virtual`</small>


#### <a name="BEGDC3CFF10"></a>BeginInvoke(string text, int index, ref [TextLayoutState](heirloom.drawing.textlayoutstate.md) state, AsyncCallback callback, object object) : IAsyncResult

<small>`Virtual`</small>


#### <a name="END32602838"></a>EndInvoke(ref [TextLayoutState](heirloom.drawing.textlayoutstate.md) state, IAsyncResult result) : void

<small>`Virtual`</small>


