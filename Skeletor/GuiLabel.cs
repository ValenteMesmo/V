using Microsoft.Xna.Framework;
using MonogameFacade;

namespace Skeletor
{
    public class GuiLabel : GameObject, IPoolable
    {
        private TextRenderer TextRenderer = null;

        public GuiLabel()
        {
            TextRenderer = new TextRenderer();
            this.Renderers.Add(TextRenderer);
        }

        public void Reset()
        {
            TextRenderer.Text = "";
            Location = Point.Zero;
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
