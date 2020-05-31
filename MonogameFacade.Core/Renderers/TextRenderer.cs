﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameFacade
{
    public class GuiTextRenderer : Renderer
    {
        public SpriteFont Font;
        public string Text;
        public float scale;
        public Color Color;

        public GuiTextRenderer()
        {
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
    }

    public class TextRenderer : Renderer
    {
        public SpriteFont Font;
        public string Text;
        public float scale;
        public Color Color;
        public Point Location;

        public TextRenderer()
        {
            Color = Color.Red;
            scale = 10;
        }

        public override void Draw(SpriteBatch batchGui, SpriteBatch batch, GameObject Parent)
        {
            batch.DrawString(
                Font
                , Text
                , (Parent.Location + Location).ToVector2()
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
