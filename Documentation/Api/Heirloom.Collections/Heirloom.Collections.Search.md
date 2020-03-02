# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## Search (Static Class)
<small>**Namespace**: Heirloom.Collections</small>  

| Methods                            | Summary |
|------------------------------------|---------|
| [HeuristicSearch<T>](#HEU668184EA) |         |
| [HeuristicSearch<T>](#HEU34D6C75A) |         |
| [HeuristicSearch<T>](#HEU3FF92BED) |         |
| [DepthFirst<T>](#DEP46C8465)       |         |
| [BreadthFirst<T>](#BRE34FBBA20)    |         |
| [IsAcyclicGraph<T>](#ISAC16903BD)  |         |

### Methods

#### <a name="HEU668184EA"></a>HeuristicSearch<T>(T start, T goal, Func\<T, IEnumerable\<T>> getSuccessors, [Search.CostFunction\<T>](Heirloom.Collections.Search.CostFunction[T].md) cost) : IList\<T>
<small>`Static`</small>


#### <a name="HEU34D6C75A"></a>HeuristicSearch<T>(T start, T goal, Func\<T, IEnumerable\<T>> getSuccessors, [Search.CostFunction\<T>](Heirloom.Collections.Search.CostFunction[T].md) cost, [Search.CostFunction\<T>](Heirloom.Collections.Search.CostFunction[T].md) heuristic) : IList\<T>
<small>`Static`</small>


#### <a name="HEU3FF92BED"></a>HeuristicSearch<T>(T start, Func\<T, bool> targetPredicate, Func\<T, IEnumerable\<T>> getSuccessors, [Search.CostFunction\<T>](Heirloom.Collections.Search.CostFunction[T].md) cost, [Search.HeuristicFunction\<T>](Heirloom.Collections.Search.HeuristicFunction[T].md) heuristic) : IList\<T>
<small>`Static`</small>


#### <a name="DEP46C8465"></a>DepthFirst<T>(T start, Func\<T, IEnumerable\<T>> getSuccessors) : IEnumerable\<T>
<small>`Static`, `IteratorStateMachineAttribute`</small>


#### <a name="BRE34FBBA20"></a>BreadthFirst<T>(T start, Func\<T, IEnumerable\<T>> getSuccessors) : IEnumerable\<T>
<small>`Static`, `IteratorStateMachineAttribute`</small>


#### <a name="ISAC16903BD"></a>IsAcyclicGraph<T>(T start, Func\<T, IEnumerable\<T>> getSuccessors) : bool
<small>`Static`</small>


