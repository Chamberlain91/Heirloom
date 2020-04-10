# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../Heirloom.Collections.Spatial/Heirloom.Collections.Spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## GridNeighborType (Enum)
<small>**Namespace**: Heirloom.Collections.Spatial</small>  
<small>**Interfaces**: IComparable, IFormattable, IConvertible</small>  

Describes the choice of neighbors in a grid.

### Values

#### Axis
<member name="F:Heirloom.Collections.Spatial.GridNeighborType.Axis">
  <summary>
            The four neighbors to north, east, south, west.
            </summary>
</member>

#### Diagonal
<member name="F:Heirloom.Collections.Spatial.GridNeighborType.Diagonal">
  <summary>
            The four neighbors to north-east, south-east, south-west, north-west.
            </summary>
</member>

#### All
<member name="F:Heirloom.Collections.Spatial.GridNeighborType.All">
  <summary>
            All eight neiboring tiles (combines <see cref="F:Heirloom.Collections.Spatial.GridNeighborType.Axis" /> and <see cref="F:Heirloom.Collections.Spatial.GridNeighborType.Diagonal" />).
            </summary>
</member>

