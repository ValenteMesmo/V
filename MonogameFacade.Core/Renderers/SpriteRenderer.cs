using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameFacade
{
    public class SpriteRenderer : Renderer
    {
        public Texture2D Texture;
        public Color Color;
        public Rectangle? Source;
        private Rectangle Target;

        public float Rotation;
        public Vector2 RotationCenter; 

        public Point Offset;
        public Point Size;

        public SpriteRenderer()
        {
            Color = Color.White;
        }

        public override void Draw(SpriteBatch batchUi, SpriteBatch batch, GameObject Parent)
        {
            Target.Location = Offset + Parent.Location;
            Target.Size = Size;

            batch.Draw(
                Texture
                , Target
                , Source
                , Color
                , Rotation
                , RotationCenter
                , SpriteEffects.None
                , 0
            );
        }
    }

    public class GuiSpriteRenderer : Renderer
    {
        public Texture2D Texture;
        public Color Color;
        public Rectangle? Source;
        private Rectangle Target;

        public Point Offset;
        public Point Size;

        public GuiSpriteRenderer()
        {
            Color = Color.White;
        }

        public override void Draw(SpriteBatch batchGui, SpriteBatch batch, GameObject Parent)
        {
            Target.Location = Offset + Parent.Location;
            Target.Size = Size;
            
            batchGui.Draw(
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
