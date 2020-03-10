using Microsoft.Xna.Framework;
using MonogameFacade;

namespace V.Desktop
{
    public class Game1 : DesktopGame
    {
        protected override void LoadContent()
        {
            base.LoadContent();            
            Objects.Add(new WorldBuilder(this));            
        }
    }
}
