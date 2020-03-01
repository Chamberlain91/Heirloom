# Heirloom.OpenGLES

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.OpenGLES](../heirloom.opengles/heirloom.opengles.md)</small>  

## StencilOperation (Enum)
<small>**Namespace**: Heirloom.OpenGLES</sub></small>  
<small>**Interfaces**: IComparable, IFormattable, IConvertible</small>  

### Values

#### Zero
<member name="F:Heirloom.OpenGLES.StencilOperation.Zero">
  <summary>
            Sets the stencil buffer value to 0.
            </summary>
</member>

#### Invert
<member name="F:Heirloom.OpenGLES.StencilOperation.Invert">
  <summary>
            Bitwise inverts the current stencil buffer value.
            </summary>
</member>

#### Keep
<member name="F:Heirloom.OpenGLES.StencilOperation.Keep">
  <summary>
            Keeps the current value.
            </summary>
</member>

#### Replace
<member name="F:Heirloom.OpenGLES.StencilOperation.Replace">
  <summary>
            Sets the stencil buffer value to ref, as specified by <see cref="!:GL.StencilFunc(StencilFunction, int, uint)" />.
            </summary>
</member>

#### Increment
<member name="F:Heirloom.OpenGLES.StencilOperation.Increment">
  <summary>
            Increments the current stencil buffer value. Clamps to the maximum representable unsigned value.
            </summary>
</member>

#### Decrement
<member name="F:Heirloom.OpenGLES.StencilOperation.Decrement">
  <summary>
            Decrements the current stencil buffer value. Clamps to 0.
            </summary>
</member>

#### IncrementWrap
<member name="F:Heirloom.OpenGLES.StencilOperation.IncrementWrap">
  <summary>
            Increments the current stencil buffer value. Wraps stencil buffer value to zero when incrementing the maximum representable unsigned value.
            </summary>
</member>

#### DecrementWrap
<member name="F:Heirloom.OpenGLES.StencilOperation.DecrementWrap">
  <summary>
            Decrements the current stencil buffer value. Wraps stencil buffer value to the maximum representable unsigned value when decrementing a stencil buffer value of zero.
            </summary>
</member>

