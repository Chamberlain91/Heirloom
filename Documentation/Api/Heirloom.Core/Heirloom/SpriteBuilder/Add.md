# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## SpriteBuilder.Add

> **Namespace**: [Heirloom][0]  
> **Type**: [SpriteBuilder][1]  

### Add(string, Image)

Add a single image animation.

```cs
public void Add(string name, Image frame)
```

### Add(string, float, params Image[])

```cs
public void Add(string name, float frameDelay, params Image[] frames)
```

### Add(string, float, IEnumerable<Image>)

Adds a new animation to the builder from multiple images.

```cs
public void Add(string name, float frameDelay, IEnumerable<Image> frames)
```

### Add(string, float, Sprite.Direction, params Image[])

```cs
public void Add(string name, float frameDelay, Sprite.Direction direction, params Image[] frames)
```

### Add(string, float, Sprite.Direction, IEnumerable<Image>)

```cs
public void Add(string name, float frameDelay, Sprite.Direction direction, IEnumerable<Image> frames)
```

[0]: ../../../Heirloom.Core.md
[1]: ../SpriteBuilder.md
