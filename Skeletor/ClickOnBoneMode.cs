using MonogameFacade;

namespace Skeletor
{
    public class ClickOnBoneMode : GameObject
    {
        private readonly DisplayMode mode = null;

        public ClickOnBoneMode(DisplayMode mode)
        {
            this.mode = mode;
        }

        public override void Update(BaseGame game)
        {
            if (mode.mode != DisplayModeEnum.Bone)
                return;

            if (game.MouseInput.LeftButton == BtnState.Pressing)
                game.Objects.Add(
                    new BoneCreationIntent(game, game
                            .MouseInput
                            .WorldPosition));

        }
    }
}
