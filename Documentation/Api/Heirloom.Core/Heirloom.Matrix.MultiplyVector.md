# Matrix.MultiplyVector

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Matrix][1]

--------------------------------------------------------------------------------

### MultiplyVector(in Vector)

Multiplies a vector against this matrix ignoring the translational components.

```cs
public Vector MultiplyVector(in Vector v)
```

### MultiplyVector(in Matrix, in Vector, ref Vector)

Multiplies a vector and matrix together ignoring the translational components and stores the resulting vector into `dest` .

```cs
public void MultiplyVector(in Matrix a, in Vector v, ref Vector r)
```

### MultiplyVector(in Matrix, in Vector)

Multiplies a vector and matrix together ignoring the translational components.

```cs
public Vector MultiplyVector(in Matrix a, in Vector v)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.Matrix.md
