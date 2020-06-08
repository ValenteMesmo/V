using Android.Content.Res;
using MonogameFacade;

namespace V.Android
{
    public class Game1 : AndroidGame
    {
        public Game1(AssetManager assets) : base(assets)
        {
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            Objects.Add(new WorldBuilder(this));
        }
    }
}

