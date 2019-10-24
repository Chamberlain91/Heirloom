using System;

namespace Heirloom.Sound.Effects
{
    /// <summary>
    /// A mixer effect that implements a simple echo.
    /// </summary>
    public class DelayEffect : AudioEffect
    {
        private float[] _echo = Array.Empty<float>();
        private int _time;

        private readonly LowPassFilter _lpf;

        public DelayEffect(float delay, float decay = 0.5F, float mix = 1.0F, float lowPassCutoff = 11025F)
        {
            _lpf = new LowPassFilter(lowPassCutoff);

            Delay = delay;
            Decay = decay;
            Mix = mix;
        }

        public float Delay { get; set; }

        public float Decay { get; set; }

        public float Mix { get; set; }

        public float LowPassCutoff
        {
            get => _lpf.Cutoff;
            set => _lpf.Cutoff = value;
        }

        protected internal override void MixOutput(Span<float> samples)
        {
            // Compute delay in samples and ensure buffer is long enough
            var echoBufferSize = GetDurationInSamples(Delay) + AudioMixer.Channels;
            if (_echo.Length < echoBufferSize) { Array.Resize(ref _echo, echoBufferSize); }

            // Process samples
            // todo: I think collectively does not properly conservere the stereo channels
            for (var i = 0; i < samples.Length; i++)
            {
                var e = (_time + i) % _echo.Length;
                var ef = (_time + i + GetDurationInSamples(Delay)) % _echo.Length;

                // Compute echo
                var echo = (_echo[e] + samples[i]) * Decay;

                // Write the echo into the future
                _echo[ef] += echo;

                // Process echo
                // todo: Does not properly conservere the stereo channels
                _lpf.MixOutput(new Span<float>(_echo, ef, 1));

                // Mix echo buffer into stream
                samples[i] = Interpolate(samples[i], echo, Mix);

                // Clear because it is the past now
                _echo[(_time + i) % _echo.Length] = 0;
            }

            // Advance time
            _time += samples.Length;
            if (_time >= _echo.Length) { _time -= _echo.Length; }
        }

        private static int GetDurationInSamples(float delay)
        {
            return (int) (delay * AudioMixer.SampleRate) * AudioMixer.Channels;
        }
    }
}
