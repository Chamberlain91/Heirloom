# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## DrawingPerformance (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</small>  

Contains information pertaining to draw performance.

| Properties                    | Summary                                                                           |
|-------------------------------|-----------------------------------------------------------------------------------|
| [OverlayMode](#OVE51B3EE7D)   | Gets or sets a value that will enable or disable drawing the performance overlay. |
| [BatchCount](#BAT27B69C73)    | Statistics of the number of batches.                                              |
| [DrawCount](#DRA5740BB87)     | Statistics of the number of 'things' drawn.                                       |
| [TriangleCount](#TRIFB928221) | Statistics of the number of triangles.                                            |
| [FrameRate](#FRA55D83BCF)     | Statistics of the frame rate.                                                     |

### Constructors

#### DrawingPerformance()

### Properties

#### <a name="OVE51B3EE7D"></a>OverlayMode : [PerformanceOverlayMode](Heirloom.Drawing.PerformanceOverlayMode.md)


Gets or sets a value that will enable or disable drawing the performance overlay.

#### <a name="BAT27B69C73"></a>BatchCount : [Statistics](../Heirloom.Math/Heirloom.Math.Statistics.md)

<small>`Read Only`</small>

Statistics of the number of batches.

#### <a name="DRA5740BB87"></a>DrawCount : [Statistics](../Heirloom.Math/Heirloom.Math.Statistics.md)

<small>`Read Only`</small>

Statistics of the number of 'things' drawn.

#### <a name="TRIFB928221"></a>TriangleCount : [Statistics](../Heirloom.Math/Heirloom.Math.Statistics.md)

<small>`Read Only`</small>

Statistics of the number of triangles.

#### <a name="FRA55D83BCF"></a>FrameRate : [Statistics](../Heirloom.Math/Heirloom.Math.Statistics.md)

<small>`Read Only`</small>

Statistics of the frame rate.

