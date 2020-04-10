using MonogameFacade;

namespace Skeletor
{

    public class ClickHandlerForSpriteMode : GameObject
    {
        private readonly DisplayMode mode = null;
        private SkeletonAnimationParts skeleton = null;

        public ClickHandlerForSpriteMode(DisplayMode mode, SkeletonAnimationParts skeleton)
        {
            this.mode = mode;
            this.skeleton = skeleton;
        }

        public override void Update(BaseGame game)
        {
            if (mode.mode != DisplayModeEnum.Sprite)
                return;

            if (game.MouseInput.LeftButton == BtnState.Pressing)
                skeleton.AddNewSprite(
                    game.MouseInput.WorldPosition
                    , game);
            //game.Objects.Add(
            //    new SpritePart(game)
            //    {
            //        Location = game
            //            .MouseInput.WorldPosition
            //    });

        }
    }
}
