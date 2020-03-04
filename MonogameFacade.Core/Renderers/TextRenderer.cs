using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameFacade
{
    public class TextRenderer : Renderer
    {
        public SpriteFont Font;
        public string Text;
        public float scale = 10;
        public Color Color = Color.Red;

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
