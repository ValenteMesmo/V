using Android.Content.Res;
using Microsoft.Xna.Framework;
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
            var player = new Player(this) { Location = new Point(200, 200) };
            Objects.Add(new Dpad(this, player.inputTouch));
            Objects.Add(player);
            Objects.Add(new WorldBuilder(this));
            Objects.Add(new FpsDisplay(this));
        }
    }
}

