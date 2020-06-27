using Microsoft.Xna.Framework;
using MonogameFacade;

namespace Skeletor
{
    public class GuiButton : GuiLabel
    {        
        public override void Update(MonogameFacade.Game game)
        {
            base.Update(game);

            if (new Rectangle(Location, new Point(50, 50)).Contains(game.MouseInput.Position))
                TextRenderer.Color = Color.Yellow;
            else
                TextRenderer.Color = Color.Red;
        }
    }

    public class GuiLabel : GameObject
    {
        protected TextRenderer TextRenderer = null;

        public override void Begin()
        {
            TextRenderer = new TextRenderer();
            TextRenderer.scale = 3;
            Renderers.Add(TextRenderer);
        }

        public override void End()
        {
            base.End();
            TextRenderer.Text = "";
        }

        public void SetText(string text)
        {
            TextRenderer.Text = text;
        }

        public void SetOffset(Point point)
        {
            TextRenderer.Offset = point;
        }
    }
}
