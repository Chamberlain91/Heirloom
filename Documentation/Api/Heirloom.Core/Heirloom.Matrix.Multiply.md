# Matrix.Multiply

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Matrix][1]  

--------------------------------------------------------------------------------

### Multiply(in Vector)

Multiplies a vector against this matrix.

```cs
public Vector Multiply(in Vector v)
```

### Multiply(in Matrix, in Matrix, ref Matrix)

Multiply two matrices together and store the result in `dest` .

```cs
public void Multiply(in Matrix a, in Matrix b, ref Matrix dest)
```

### Multiply(in Matrix, in Matrix)

Multiply two matrices together.

```cs
public Matrix Multiply(in Matrix a, in Matrix b)
```

### Multiply(in Matrix, in Vector, ref Vector)

Multiplies a vector and matrix together and stores the resulting vector into `dest` .

```cs
public void Multiply(in Matrix a, in Vector v, ref Vector dest)
```

### Multiply(in Matrix, in Vector)

Multiplies a vector and matrix together.

```cs
public Vector Multiply(in Matrix a, in Vector v)
```

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Matrix.md
