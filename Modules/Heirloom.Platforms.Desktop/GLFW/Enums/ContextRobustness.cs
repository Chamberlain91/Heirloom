namespace Heirloom.Platforms.Desktop
{
    internal enum ContextRobustness
    {
        None = 0,
        NoResetNotification = 0x00031001,
        LoseContextOnReset = 0x00031002
    }
}
