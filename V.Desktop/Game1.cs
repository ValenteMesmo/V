using MonogameFacade;

namespace V.Desktop
{
    public class Game1 : DesktopGame
    {
        public override void Ready(BaseGame game)
        {
            game.Objects.Add(new WorldBuilder(game));
            game.Objects.Add(new FpsDisplay(game));            
        }
    }
}
