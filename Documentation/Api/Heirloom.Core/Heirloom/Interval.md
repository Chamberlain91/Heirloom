# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Interval (Class)

> **Namespace**: [Heirloom][0]

A utility object to check if an interval of time has occured.

```cs
public sealed class Interval
```

### Properties

[Delta][1]

### Methods

[Check][2]

## Properties

#### Instance

| Name       | Type    | Summary                                             |
|------------|---------|-----------------------------------------------------|
| [Delta][1] | `float` | The time since when Check or Check was last called. |

## Methods

#### Instance

| Name                  | Return Type | Summary                                                                |
|-----------------------|-------------|------------------------------------------------------------------------|
| [Check()][2]          | `bool`      | Returns true when enough time has elapsed.                             |
| [Check(out float)][2] | `bool`      | Returns true when enough time has elapsed. Outputs the elasped time... |

[0]: ../../Heirloom.Core.md
[1]: Interval/Delta.md
[2]: Interval/Check.md
