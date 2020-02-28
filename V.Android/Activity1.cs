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
        , ScreenOrientation = ScreenOrientation.Landscape)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        private AndroidGame game;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            game = new Game1(Assets);
            SetViewFullScreen();
            game.Run();
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

            //if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P)
            {
                Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
            }

            SetContentView(view);
        }
    }
}

