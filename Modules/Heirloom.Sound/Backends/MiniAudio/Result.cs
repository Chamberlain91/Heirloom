namespace Heirloom.Sound.Backends.MiniAudio
{
    internal enum Result
    {
        Success = 0,

        /* General errors. */
        GeneralError = -1,
        InvalidArgs = -2,
        InvalidOperation = -3,
        OutOfMemory = -4,
        AccessDenied = -5,
        TooLarge = -6,
        Timeout = -7,

        /* General miniaudio-specific errors. */
        FormatNotSupported = -100,
        DeviceTypeNotSupported = -101,
        ShareModeNotSupported = -102,
        NoBackend = -103,
        NoDevice = -104,
        ApiNotFound = -105,
        InvalidDeviceConfig = -106,

        /* State errors. */
        DeviceBusy = -200,
        DeviceNotInitialized = -201,
        DeviceNotStarted = -202,
        DeviceUnavailable = -203,

        /* Operation errors. */
        FailedToMapDeviceBuffer = -300,
        FailedToUnapDeviceBuffer = -301,
        FailedToInitBackend = -302,
        FailedToReadDataFromClient = -303,
        FailedToReadDataFromDevice = -304,
        FailedToSendDataToClient = -305,
        FailedToSendDataToDevice = -306,
        FailedToOpenBackendDevice = -307,
        FailedToStartBackendDevice = -308,
        FailedToStopBackendDevice = -309,
        FailedToConfigureBackendDevice = -310,
        FailedToCreateMutex = -311,
        FailedToCreateEvent = -312,
        FailedToCreateThread = -313,
    }
}
