using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameFacade;
using System;

namespace Skeletor
{
    public class SpriteLineRenderer : Renderer
    {
        public readonly Texture2D texture = null;
        public Vector2 start;
        public Vector2 end;
        public Rectangle? source = null;

        public SpriteLineRenderer(Texture2D texture)
        {
            this.texture = texture;
        }

        public override void Draw(
            SpriteBatch batchGui
            , SpriteBatch batch
            , GameObject Parent
        )
        {
            batch.Draw(
                texture
                , start
                , new Rectangle(
                    (int)start.X
                    , (int)start.Y
                    , (int) Vector2.Distance(start, end)
                    , 100
                )
                , Color.White
                , (float)Math.Atan2(end.Y - start.Y, end.X - start.X)
                , Vector2.Zero
                , 1.0f
                , SpriteEffects.None
                , 0
            );            
        }
    }
}
