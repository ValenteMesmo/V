using Microsoft.Xna.Framework;
using MonogameFacade;

namespace Skeletor
{
    public class ClickHandlerForBoneStartMode : GameObject
    {
        private readonly DisplayMode mode = null;
        private readonly ClickHandlerForBoneEndMode handler;
        private readonly SkeletonAnimationParts skeleton = null;

        public ClickHandlerForBoneStartMode(
            DisplayMode mode
            , ClickHandlerForBoneEndMode handler
            , SkeletonAnimationParts skeleton)
        {
            this.mode = mode;
            this.handler = handler;
            this.skeleton = skeleton;
        }

        public override void Update(BaseGame game)
        {
            if (mode.mode != DisplayModeEnum.BoneStart)
                return;

            if (game.MouseInput.LeftButton == BtnState.Pressing)
            {
                var position = game.MouseInput.WorldPosition.ToVector2();
                var currentBone = skeleton.AddNewBone(position, position, game);
                handler.CurrentBone= currentBone;
                mode.mode = DisplayModeEnum.BoneEnd;
            }
        }
    }
}
