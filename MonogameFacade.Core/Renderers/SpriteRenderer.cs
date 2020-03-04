using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameFacade
{
    public class SpriteRenderer : Renderer
    {
        public Texture2D Texture;
        public Color Color = Color.White;
        public Rectangle? Source;
        private Rectangle Target;

        public Point Offset;
        public Point Size;

        public override void Draw(SpriteBatch batch, GameObject Parent)
        {
            Target.Location = Offset + Parent.Location;
            Target.Size = Size;

            batch.Draw(
                Texture
                , Target
                , Source
                , Color
                , 0
                , Vector2.Zero
                , SpriteEffects.None
                , 0
            );
        }
    }
}
