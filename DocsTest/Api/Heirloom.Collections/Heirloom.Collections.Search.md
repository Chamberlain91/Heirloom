# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## Search (Static Class)
<small>**Namespace**: Heirloom.Collections</sub></small>  

| Methods                         | Summary                                                                                                                                                          |
|---------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [HeuristicSearch<T>](#HEURD6DC) | Finds a path between two known states.                                                                                                                           |
| [HeuristicSearch<T>](#HEURD6DC) | Finds a path between two known states.                                                                                                                           |
| [HeuristicSearch<T>](#HEURD6DC) | Search a problem space from some starting state until the search predicate is satified. A common application of a heuristic search is implementing path finding. |
| [DepthFirst<T>](#DEPTCC22)      | Traverses elements in depth-first ordering.                                                                                                                      |
| [BreadthFirst<T>](#BREA73CA)    | Traverse elements in breadth-first ordering.                                                                                                                     |
| [IsAcyclicGraph<T>](#ISAC8508)  | Determines if the specified graph is acyclic. Do not use on infinite graphs.                                                                                     |

### Methods

#### <a name="HEUR170D"></a> HeuristicSearch<T>(T start, T goal, Func\<T|IEnumerable\<T>> getSuccessors, [Search.CostFunction\<T>](Heirloom.Collections.Search.CostFunction[T].md) cost) : IList\<T>
<small>`Static`</small>

Finds a path between two known states.

<small>**start**: <param name="start">Starting state.</param></small>  
<small>**goal**: <param name="goal">Terminating state.</param></small>  
<small>**getSuccessors**: <param name="getSuccessors">Returns sucessors.</param></small>  
<small>**cost**: <param name="cost">Cost function between two states. If neighbors, should return actual, if non-neighbors estimate.</param></small>  

#### <a name="HEUR1C5A"></a> HeuristicSearch<T>(T start, T goal, Func\<T|IEnumerable\<T>> getSuccessors, [Search.CostFunction\<T>](Heirloom.Collections.Search.CostFunction[T].md) cost, [Search.CostFunction\<T>](Heirloom.Collections.Search.CostFunction[T].md) heuristic) : IList\<T>
<small>`Static`</small>

Finds a path between two known states.

<small>**start**: <param name="start">Starting state.</param></small>  
<small>**goal**: <param name="goal">Terminating state.</param></small>  
<small>**getSuccessors**: <param name="getSuccessors">Returns sucessors.</param></small>  
<small>**cost**: <param name="cost">Cost function between two states.</param></small>  
<small>**heuristic**: <param name="heuristic">Estimate cost between state and goal state.</param></small>  

#### <a name="HEUR7366"></a> HeuristicSearch<T>(T start, Func\<T|bool> targetPredicate, Func\<T|IEnumerable\<T>> getSuccessors, [Search.CostFunction\<T>](Heirloom.Collections.Search.CostFunction[T].md) cost, [Search.HeuristicFunction\<T>](Heirloom.Collections.Search.HeuristicFunction[T].md) heuristic) : IList\<T>
<small>`Static`</small>

Search a problem space from some starting state until the search predicate is satified.   
 A common application of a heuristic search is implementing path finding.

<small>**start**: <param name="start">Starting state</param></small>  
<small>**targetPredicate**: <param name="targetPredicate">Search predicate</param></small>  
<small>**getSuccessors**: <param name="getSuccessors">Returns sucessors</param></small>  
<small>**cost**: <param name="cost">Cost function between two states.</param></small>  
<small>**heuristic**: <param name="heuristic">Estimate cost between state and goal state.</param></small>  

#### <a name="DEPT3488"></a> DepthFirst<T>(T start, Func\<T|IEnumerable\<T>> getSuccessors) : IEnumerable\<T>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Traverses elements in depth-first ordering.

<small>**start**: <param name="start">Starting element</param></small>  
<small>**getSuccessors**: <param name="getSuccessors">A function to return the successor/children of the current element</param></small>  

#### <a name="BREA3161"></a> BreadthFirst<T>(T start, Func\<T|IEnumerable\<T>> getSuccessors) : IEnumerable\<T>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Traverse elements in breadth-first ordering.

<small>**start**: <param name="start">Starting element.</param></small>  
<small>**getSuccessors**: <param name="getSuccessors">A function to return the successor/children of the current element.</param></small>  

#### <a name="ISAC5FBF"></a> IsAcyclicGraph<T>(T start, Func\<T|IEnumerable\<T>> getSuccessors) : bool
<small>`Static`</small>

Determines if the specified graph is acyclic. Do not use on infinite graphs.

<small>**start**: <param name="start">Starting element.</param></small>  
<small>**getSuccessors**: <param name="getSuccessors">A function to return the successor/children of the current element.</param></small>  

