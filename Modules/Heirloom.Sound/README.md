# Heirloom.Sound

Provides audio playback and capture through a simplistic object oriented model.
Create an `AudioSource` from a `AudioClip` or `Stream` pointed at a supported 
file type. You can play several sources at the same time, as should be expected 
from an game-like audio engine.

This library is written for `.Net Standard 2.1` and tested with a `.Net Core 3.0` 
console application.

### Audio Playback

```cs
using var stream = new FileStream("./mySong.mp3", FileMode.Open);
var src = new AudioSource(stream);
src.Play();

Thread.Sleep((int)(src.Duration * 1000)); // Wait for song to finish
Console.WriteLine("Thanks for listening!");
```

Additionally, both `AudioGroup` and `AudioEffect` exists for futher control over
the audio playback. An `AudioGroup` is a way of grouping `AudioSource` or other
`AudioGroup` (both are `AudioNode`) to as a whole apply effects, adjust volume
or stereo balance. The default group representing the speakers is specified by
`AudioGroup.Default` and is the default for new sources and groups.

An example of groups affecting multiple sources with a reverb effect:

```cs
var group = new AudioGroup(); // automatically a child of the default group by default
group.Effects.Add(new ReverbEffect());

// Group assignment during creation
var src1 = new AudioSource(someAudioClip, group);

// Group assignment after creation
var src2 = new AudioSource(someAudioClip);
src2.Group = group;
```

### Audio Capture

This library also supports audio capture (ie, microphone).

```cs
AudioContext.Initialize(true); // true enables audio capture
AudioContext.AudioCaptured += samples =>
{
    // Process samples somehow (ie, write to disk, stream VoIP, etc)
};
```

The samples provided in the callback are measured by the same sampling rate as
playback and governed by `AudioContext.SampleRate`. By default this value is 
`44100` samples per second (ie, hertz). The samples are provided in these 
discrete chunks as the underlying system acquires them. You can query if audio 
capture was enabled at a later point in the application by `AudioContext.IsAudioCaptureEnabled`.

----

## Version 1.1 Roadmap

#### Audio Device
- [ ] Device Enumeration
- [ ] Device Selection