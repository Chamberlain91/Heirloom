# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## DrawTextCallback (Delegate)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Inherits**: MulticastDelegate, Delegate</small>  
<small>**Interfaces**: ICloneable, ISerializable</small>  

### Constructors

#### DrawTextCallback(object object, IntPtr method)

### Methods

#### <a name="INVA48C377"></a>Invoke(string text, int index, ref [TextDrawState](heirloom.drawing.textdrawstate.md) state) : void

<small>`Virtual`</small>


#### <a name="BEG39EF3B50"></a>BeginInvoke(string text, int index, ref [TextDrawState](heirloom.drawing.textdrawstate.md) state, AsyncCallback callback, object object) : IAsyncResult

<small>`Virtual`</small>


#### <a name="END22AE23F8"></a>EndInvoke(ref [TextDrawState](heirloom.drawing.textdrawstate.md) state, IAsyncResult result) : void

<small>`Virtual`</small>


