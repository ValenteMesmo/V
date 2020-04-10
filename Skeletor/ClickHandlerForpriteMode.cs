using MonogameFacade;

namespace Skeletor
{

    public class ClickHandlerForpriteMode : GameObject
    {
        private readonly DisplayMode mode = null;

        public ClickHandlerForpriteMode(DisplayMode mode)
        {
            this.mode = mode;
        }

        public override void Update(BaseGame game)
        {
            if (mode.mode != DisplayModeEnum.Sprite)
                return;

            if(game.MouseInput.LeftButton == BtnState.Pressing)
                game.Objects.Add(
                    new SpritePart(game)
                    {
                        Location = game
                            .MouseInput.WorldPosition
                    });

        }
    }
}
