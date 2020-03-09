using Microsoft.Xna.Framework;
using MonogameFacade;

namespace V.Desktop
{
    public class Game1 : DesktopGame
    {
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
