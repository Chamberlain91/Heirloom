using Android.App;
using Android.Util;
using Android.Views;

using Heirloom.Math;

namespace Heirloom.Android
{
    public static class AndroidHelper
    {
        public static void SetWindowFullscreen(Activity activity)
        {
            // Set application to fullscreen
            activity.RequestWindowFeature(WindowFeatures.NoTitle);
            activity.Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
        }

        public static IntSize ComputeAutomaticResolution(Activity activity)
        {
            var metrics = new DisplayMetrics();
            activity.WindowManager.DefaultDisplay.GetRealMetrics(metrics);

            return ComputeAutomaticResolution(metrics.WidthPixels, metrics.HeightPixels, (float) metrics.DensityDpi);
        }

        public static IntSize ComputeAutomaticResolution(int width, int height, float dpi)
        {
            const float MillimetersPerInch = 25.4F; // mmpi
            const int PixelsPerMillimeter = 10;     // dpmm 

            var wPX = width;
            var hPX = height;

            var wIN = wPX / dpi;
            var hIN = hPX / dpi;

            var wMM = wIN * MillimetersPerInch;
            var hMM = hIN * MillimetersPerInch;

            // Compute pixels per millimeter
            var wQQ = Calc.Floor(wMM * PixelsPerMillimeter);
            var hQQ = Calc.Floor(hMM * PixelsPerMillimeter);

            // Note: We clamp here to ensure we never go more dense than the actual pixels on the screen.
            return (width: Calc.Min(width, wQQ), height: Calc.Min(height, hQQ));
        }
    }
}
