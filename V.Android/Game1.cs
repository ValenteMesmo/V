using Android.Content.Res;
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
            game.Objects.Add(new FpsDisplay(game));
        }
    }
}

