# AudioClip

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

An object to contain (and decode) audio data into raw samples.

```cs
public sealed class AudioClip
```

--------------------------------------------------------------------------------

**Properties**: [Item][1], [Duration][2], [Length][3]

--------------------------------------------------------------------------------

## Constructors

### AudioClip(Stream)

Constructs a new audio clip from the given stream, fully decoding all samples.

```cs
public AudioClip(Stream stream)
```

### AudioClip(byte[])

```cs
public AudioClip(byte[] file)
```

### AudioClip(short[])

```cs
public AudioClip(short[] samples)
```

## Properties

| Name          | Summary                                   |
|---------------|-------------------------------------------|
| [Item][1]     |                                           |
| [Duration][2] | Gets the duration of the clip in seconds. |
| [Length][3]   | Gets the length of the clip in samples.   |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.AudioClip.Item.md
[2]: Heirloom.AudioClip.Duration.md
[3]: Heirloom.AudioClip.Length.md
