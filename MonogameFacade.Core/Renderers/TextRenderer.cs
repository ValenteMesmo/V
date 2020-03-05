using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameFacade
{
    public class TextRenderer : Renderer
    {
        public SpriteFont Font;
        public string Text;
        public float scale;
        public Color Color;

        public TextRenderer()
        {
            Color = Color.Red;
            scale = 10;
        }

        public override void Draw(SpriteBatch batch, GameObject Parent)
        {
            batch.DrawString(
                Font
                , Text
                , Parent.Location.ToVector2()
                , Color
                , 0
                , Vector2.Zero
                , scale
                , SpriteEffects.None
                , 0
            );
        }
    }
}
