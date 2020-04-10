using MonogameFacade;

namespace Skeletor
{

    public class Game1 : DesktopGame
    {
        protected override void LoadContent()
        {
            base.LoadContent();

            var mode = new DisplayMode(this);
            var skeleton = new SkeletonAnimationParts();
            var boneHandler = new ClickHandlerForBoneEndMode(mode, skeleton);
            Objects.Add(mode);
            Objects.Add(new ClickHandlerForSpriteMode(mode, skeleton));
            Objects.Add(new ClickHandlerForBoneStartMode(mode, boneHandler, skeleton));
            Objects.Add(boneHandler);
            Objects.Add(new ClickHnadlerForMoveMode(mode, this));
        }
    }
}
