# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## TextLayoutCallback (Delegate)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Inherits**: MulticastDelegate, Delegate</small>  
<small>**Interfaces**: ICloneable, ISerializable</small>  

### Constructors

#### TextLayoutCallback(object object, IntPtr method)

### Methods

#### <a name="INV1C27C217"></a>Invoke(string text, int index, ref [TextLayoutState](Heirloom.Drawing.TextLayoutState.md) state) : void

<small>`Virtual`</small>


#### <a name="BEGF7D2B0B0"></a>BeginInvoke(string text, int index, ref [TextLayoutState](Heirloom.Drawing.TextLayoutState.md) state, AsyncCallback callback, object object) : IAsyncResult

<small>`Virtual`</small>


#### <a name="ENDBAA0C958"></a>EndInvoke(ref [TextLayoutState](Heirloom.Drawing.TextLayoutState.md) state, IAsyncResult result) : void

<small>`Virtual`</small>


