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

        public override void Ready(BaseGame game)
        {
            game.Objects.Add(new Player(game) { Location = new Point(200, 200) });
            game.Objects.Add(new WorldBuilder(game));
            game.Objects.Add(new FpsDisplay(game));
            game.Objects.Add(new Dpad(game));
        }
    }
}

