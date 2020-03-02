# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## DrawTextCallback (Delegate)
<small>**Namespace**: Heirloom.Drawing</small>  
<small>**Inherits**: MulticastDelegate, Delegate</small>  
<small>**Interfaces**: ICloneable, ISerializable</small>  

### Constructors

#### DrawTextCallback(object object, IntPtr method)

### Methods

#### <a name="INVF51AD57"></a>Invoke(string text, int index, ref [TextDrawState](Heirloom.Drawing.TextDrawState.md) state) : void
<small>`Virtual`</small>


#### <a name="BEG6760AD30"></a>BeginInvoke(string text, int index, ref [TextDrawState](Heirloom.Drawing.TextDrawState.md) state, AsyncCallback callback, object object) : IAsyncResult
<small>`Virtual`</small>


#### <a name="END2643118"></a>EndInvoke(ref [TextDrawState](Heirloom.Drawing.TextDrawState.md) state, IAsyncResult result) : void
<small>`Virtual`</small>


