namespace Heirloom.Sound
{
    public interface IAudioProvider
    {
        int Position { get; }

        int Length { get; }

        bool CanSeek { get; }

        int ReadSamples(short[] samples, int offset, int count);

        void Seek(int offset);
    }
}
