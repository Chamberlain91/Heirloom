# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## Search (Static Class)
<small>**Namespace**: Heirloom.Collections</sub></small>  

| Methods | Summary |
|---------|---------|
| [HeuristicSearch<T>](#HEU170D32C0) | Finds a path between two known states. |
| [HeuristicSearch<T>](#HEU1C5A8970) | Finds a path between two known states. |
| [HeuristicSearch<T>](#HEU73668FF9) | Search a problem space from some starting state until the search predicate is satified. A common application of a heuristic search is implementing path finding. |
| [DepthFirst<T>](#DEP348808B1) | Traverses elements in depth-first ordering. |
| [BreadthFirst<T>](#BRE31612DF4) | Traverse elements in breadth-first ordering. |
| [IsAcyclicGraph<T>](#ISA5FBFA081) | Determines if the specified graph is acyclic. Do not use on infinite graphs. |

### Methods

#### <a name="HEU170D32C0"></a>HeuristicSearch<T>(T start, T goal, Func\<T|IEnumerable\<T>> getSuccessors, [Search.CostFunction\<T>](Heirloom.Collections.Search.CostFunction[T].md) cost) : IList\<T>

<small>`Static`</small>

Finds a path between two known states.

<small>**start**: <param name="start">Starting state.</param>  
</small>
<small>**goal**: <param name="goal">Terminating state.</param>  
</small>
<small>**getSuccessors**: <param name="getSuccessors">Returns sucessors.</param>  
</small>
<small>**cost**: <param name="cost">Cost function between two states. If neighbors, should return actual, if non-neighbors estimate.</param>  
</small>

#### <a name="HEU1C5A8970"></a>HeuristicSearch<T>(T start, T goal, Func\<T|IEnumerable\<T>> getSuccessors, [Search.CostFunction\<T>](Heirloom.Collections.Search.CostFunction[T].md) cost, [Search.CostFunction\<T>](Heirloom.Collections.Search.CostFunction[T].md) heuristic) : IList\<T>

<small>`Static`</small>

Finds a path between two known states.

<small>**start**: <param name="start">Starting state.</param>  
</small>
<small>**goal**: <param name="goal">Terminating state.</param>  
</small>
<small>**getSuccessors**: <param name="getSuccessors">Returns sucessors.</param>  
</small>
<small>**cost**: <param name="cost">Cost function between two states.</param>  
</small>
<small>**heuristic**: <param name="heuristic">Estimate cost between state and goal state.</param>  
</small>

#### <a name="HEU73668FF9"></a>HeuristicSearch<T>(T start, Func\<T|bool> targetPredicate, Func\<T|IEnumerable\<T>> getSuccessors, [Search.CostFunction\<T>](Heirloom.Collections.Search.CostFunction[T].md) cost, [Search.HeuristicFunction\<T>](Heirloom.Collections.Search.HeuristicFunction[T].md) heuristic) : IList\<T>

<small>`Static`</small>

Search a problem space from some starting state until the search predicate is satified.   
 A common application of a heuristic search is implementing path finding.

<small>**start**: <param name="start">Starting state</param>  
</small>
<small>**targetPredicate**: <param name="targetPredicate">Search predicate</param>  
</small>
<small>**getSuccessors**: <param name="getSuccessors">Returns sucessors</param>  
</small>
<small>**cost**: <param name="cost">Cost function between two states.</param>  
</small>
<small>**heuristic**: <param name="heuristic">Estimate cost between state and goal state.</param>  
</small>

#### <a name="DEP348808B1"></a>DepthFirst<T>(T start, Func\<T|IEnumerable\<T>> getSuccessors) : IEnumerable\<T>

<small>`Static`, `IteratorStateMachineAttribute`</small>

Traverses elements in depth-first ordering.

<small>**start**: <param name="start">Starting element</param>  
</small>
<small>**getSuccessors**: <param name="getSuccessors">A function to return the successor/children of the current element</param>  
</small>

#### <a name="BRE31612DF4"></a>BreadthFirst<T>(T start, Func\<T|IEnumerable\<T>> getSuccessors) : IEnumerable\<T>

<small>`Static`, `IteratorStateMachineAttribute`</small>

Traverse elements in breadth-first ordering.

<small>**start**: <param name="start">Starting element.</param>  
</small>
<small>**getSuccessors**: <param name="getSuccessors">A function to return the successor/children of the current element.</param>  
</small>

#### <a name="ISA5FBFA081"></a>IsAcyclicGraph<T>(T start, Func\<T|IEnumerable\<T>> getSuccessors) : bool

<small>`Static`</small>

Determines if the specified graph is acyclic. Do not use on infinite graphs.

<small>**start**: <param name="start">Starting element.</param>  
</small>
<small>**getSuccessors**: <param name="getSuccessors">A function to return the successor/children of the current element.</param>  
</small>

