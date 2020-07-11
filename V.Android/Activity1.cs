using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using MonogameFacade;

namespace V.Android
{
    [Activity(Label = "V"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ResizeableActivity = true
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        private AndroidGame game = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            game = new Game1(Assets);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                Vibrator vibrator = (Vibrator)GetSystemService(VibratorService);
                game.Vibrate = f => vibrator.Vibrate(VibrationEffect.CreateOneShot(f, 1));
            }
            else
            {
                //TODO:   
            }

            SetViewFullScreen();
            game.Run();
        }

        protected override void OnResume()
        {
            base.OnResume();
            SetViewFullScreen();
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            SetViewFullScreen();
        }

        private void SetViewFullScreen()
        {
            var view = (View)game.Services.GetService(typeof(View));
            view.SystemUiVisibility = (StatusBarVisibility)
                (SystemUiFlags.LayoutStable
                | SystemUiFlags.LayoutHideNavigation
                | SystemUiFlags.LayoutFullscreen
                | SystemUiFlags.HideNavigation
                | SystemUiFlags.Fullscreen
                | SystemUiFlags.ImmersiveSticky);

            Window.AddFlags(WindowManagerFlags.Fullscreen);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
            {
                Window.Attributes.LayoutInDisplayCutoutMode =
                LayoutInDisplayCutoutMode.ShortEdges;
            }

            SetContentView(view);
        }
    }
}

