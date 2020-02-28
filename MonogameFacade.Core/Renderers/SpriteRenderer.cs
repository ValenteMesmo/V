using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameFacade
{
    public class SpriteRenderer : Renderer
    {
        public Texture2D Texture;
        public Color Color = Color.White;
        public Rectangle? Source;
        public Rectangle Target;

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(Texture, Target, Source, Color, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
