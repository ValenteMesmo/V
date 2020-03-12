using Microsoft.Xna.Framework.Input;
using MonogameFacade;

namespace Skeletor
{
    public class DisplayMode : GameObject
    {
        public DisplayModeEnum mode ;
        private TextRenderer text;

        public DisplayMode(BaseGame game)
        {
            text = new TextRenderer();
            text.Text = "";
            text.Font = game.Font;

            Renderers.Add(text);
        }

        public override void Update(BaseGame game)
        {
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.D1))
                mode = DisplayModeEnum.Sprite;
            if (keyboard.IsKeyDown(Keys.D2))
                mode = DisplayModeEnum.Bone;
            if (keyboard.IsKeyDown(Keys.D3))
                mode = DisplayModeEnum.Move;
            if (keyboard.IsKeyDown(Keys.D4))
                mode = DisplayModeEnum.Erase;

            text.Text = mode.ToString();
        }
    }
}
