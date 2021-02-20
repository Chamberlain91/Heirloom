using Android.App;
using Android.Content;
using Android.Content.PM;

using Heirloom.Android;

namespace Heirloom.Benchmark.Android
{
    [Activity(Label = "Benchmark",
        ScreenOrientation = ScreenOrientation.SensorLandscape,
        ConfigurationChanges = ConfigChanges.Orientation,
        MainLauncher = true)]
    public class MainActivity : GraphicsActivity
    {
        public BenchmarkApp App;

        protected override void GetUserGraphicsConfiguration(ref GraphicsConfiguration config)
        {
            config.VSync = false;
            config.Resolution = (1280, 720);
        }

        protected override void GraphicsResume()
        {
            if (App == null)
            {
                App = new BenchmarkApp(Graphics)
                {
                    GetRecordText = GetRecordText,
                    SetRecordText = SetRecordText
                };
            }

            App.Loop.Start();
        }

        protected override void GraphicsPause()
        {
            App.Loop.Stop();
        }

        private void SetRecordText(string message)
        {
            var preferences = GetPreferences(FileCreationMode.Private);
            var editor = preferences.Edit();
            editor.PutString("record", message);
            editor.Apply();
        }

        private string GetRecordText()
        {
            var preferences = GetPreferences(FileCreationMode.Private);
            return preferences.GetString("record", string.Empty);
        }
    }
}
