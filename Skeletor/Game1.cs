using MonogameFacade;

namespace Skeletor
{

    public class Game1 : DesktopGame
    {
        protected override void LoadContent()
        {
            base.LoadContent();

            var mode = new DisplayMode(this);
            //Objects.Add(new AddSpriteButton(this));
            Objects.Add(mode);
            Objects.Add(new ClickOnSpriteMode(mode));
            Objects.Add(new ClickOnBoneMode(mode));
            
        }
    }
}
