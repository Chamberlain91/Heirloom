# Heirloom.Sound

Audio Playback and Capture

----

## 1.0 Road Map

### High Level Features

#### AudioClip

- Creation
  - [ ] `new AudioClip(short[] samples)`
  - [ ] `AudioClip.FromStream(Stream stream)`
  - [ ] `AudioClip.FromFile(byte[] file)`
- Properties
  - [ ] `IReadOnlyList<short> Samples { get; }`
  - [ ] `float Duration { get; }`

#### AudioSource

- Creation
  - [ ] `new AudioSource(AudioClip clip)`
  - [ ] `new AudioSource(Stream stream)`
- Mixing 
  - [ ] `float Volume { get; }`
  - [ ] `float Balance { get; }`
- Playback
  - [ ] `int Length { get; }`
  - [ ] `float Duration { get; }`
  - [ ] `bool IsLooping { get; }`
  - [ ] `bool CanSeek { get; }`
  - [ ] `void Play()`
  - [ ] `void Pause()`
  - [ ] `void Stop()` -> `Pause(); if (CanSeek) { Seek(0); }`
  - [ ] `void Seek(float time)`
  - [ ] `void Seek(int sample)`
- Events
  - [ ] `PlaybackEnded`

### Low Level Features

#### Audio Device
- [ ] Device Enumeration

#### Audio Procesor
- [ ] Playback
- [ ] Capture

#### Audio Decoder