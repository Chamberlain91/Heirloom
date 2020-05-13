using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Heirloom.Desktop")]   // Application implementation on Desktop (uses Heirloom.OpenGLES, Heirloom.MiniAudio)
[assembly: InternalsVisibleTo("Heirloom.Android")]   // Application implementation on Android (uses Heirloom.OpenGLES)
[assembly: InternalsVisibleTo("Heirloom.OpenGLES")]  // Graphics implementation via OpenGL ES 3.0
[assembly: InternalsVisibleTo("Heirloom.Vulkan")]    // Graphics implementation via Vulkan (n/a)
