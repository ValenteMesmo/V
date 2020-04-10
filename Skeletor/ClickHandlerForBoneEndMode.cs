using MonogameFacade;

namespace Skeletor
{
    public class ClickHandlerForBoneEndMode : GameObject
    {
        private readonly DisplayMode mode = null;
        private readonly SkeletonAnimationParts skeleton = null;
        public Bone CurrentBone = null;

        public ClickHandlerForBoneEndMode(
            DisplayMode mode
            , SkeletonAnimationParts skeleton)
        {
            this.mode = mode;
            this.skeleton = skeleton;
        }

        public override void Update(BaseGame game)
        {
            if (mode.mode != DisplayModeEnum.BoneEnd)
                return;

            CurrentBone.BoneSprite.end = game.MouseInput.WorldPosition.ToVector2();
            if (game.MouseInput.LeftButton == BtnState.Released)
            {
                mode.mode = DisplayModeEnum.BoneStart;
            }

        }
    }
}
