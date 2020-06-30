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

        private static Pool<GuiTextRenderer> Pool = new Pool<GuiTextRenderer>();

        public override void ReturnToPool()
        {
            Font = Game.Instance.Font;
            Color = Color.Red;
            scale = 10;
            Text = "";
            Pool.Return(this);
        }

        public GuiTextRenderer GetFromPool()
        {
            return Pool.Get();
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
    }

    public class TextRenderer : Renderer
    {
        private SpriteFont Font;
        public string Text;
        public float scale;
        public Color Color;
        public Point Offset;

        private static Pool<TextRenderer> Pool = new Pool<TextRenderer>();

        public override void ReturnToPool()
        {

            Color = Color.Red;
            scale = 10;
            Offset = Point.Zero;
            Font = Game.Instance.Font;
            Pool.Return(this);
        }

        public static TextRenderer GetFromPool()
        {
            return Pool.Get();
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
    }
}
