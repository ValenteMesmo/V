using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameFacade
{
    public class GuiTextRenderer : Renderer
    {
        private SpriteFont Font;
        public string Text;
        public float scale;
        public Color Color;

        public override void Begin()
        {
            base.Begin();
            Font = Game.Instance.Font;
            Color = Color.Red;
            scale = 10;
        }

        public override void Draw(SpriteBatch batchGui, SpriteBatch batch, GameObject Parent)
        {
            batchGui.DrawString(
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

        public override void End()
        {
            Color = Color.Red;
            scale = 10;
        }
    }

    public class TextRenderer : Renderer
    {
        private SpriteFont Font;
        public string Text;
        public float scale;
        public Color Color;
        public Point Offset;

        public override void Begin()
        {
            base.Begin();
            Color = Color.Red;
            scale = 10;
            Font = Game.Instance.Font;
        }

        public override void Draw(SpriteBatch batchGui, SpriteBatch batch, GameObject Parent)
        {
            batch.DrawString(
                Font
                , Text
                , (Parent.Location + Offset).ToVector2()
                , Color
                , 0
                , Vector2.Zero
                , scale
                , SpriteEffects.None
                , 0
            );
        }

        public override void End()
        {
            Color = Color.Red;
            scale = 10;
            Offset = Point.Zero;
        }
    }
}
