using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameFacade;
using System;

namespace Skeletor
{
    public class SpriteLineRenderer : Renderer
    {
        public readonly Texture2D texture = null;
        private readonly BaseGame game = null;
        public Vector2 start;
        public Vector2 end;
        public Rectangle? source = null;

        public SpriteLineRenderer(Texture2D texture, BaseGame game)
        {
            this.texture = texture;
            this.game = game;
        }

        public override void Draw(SpriteBatch batchGui, SpriteBatch batch, GameObject Parent)
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

    public class BoneCreationIntent : GameObject
    {
        private SpriteLineRenderer sprite;
        public BoneCreationIntent(BaseGame game, Point offset)
        {
            sprite = new SpriteLineRenderer(game.Textures["btn"], game);
            sprite.end = sprite.start = offset.ToVector2();
            //sprite.Color = Color.Yellow;
            //begin = sprite.Offset = offset;
            //sprite.Size = new Point(1000);
            Renderers.Add(sprite);

            //sprite.RotationCenter =  new Vector2(100,100);
            //sprite.RotationCenter = new Vector2(100, 0);
        }

        public override void Update(BaseGame game)
        {
            //var distance = game.MouseInput.WorldPosition - Location;
            sprite.end = game.MouseInput.WorldPosition.ToVector2();
            //sprite.Rotation = (float)
            //(
            //    Math.Atan2(
            //        game.MouseInput.WorldPosition.Y - begin.Y
            //        , game.MouseInput.WorldPosition.X - begin.X) //+ (Math.PI*0.5f)
            //);
            //spriteBatch.Draw(texture, start, null, Color.White,
            //    (float)Math.Atan2(end.Y - start.Y, end.X - start.X),
            //    new Vector2(0f, (float)texture.Height / 2),
            //    new Vector2(Vector2.Distance(start, end), 1f),
            //    SpriteEffects.None, 0f);

            //sprite.Size = distance;
        }
    }

    public class ClickOnBoneMode : GameObject
    {
        private readonly DisplayMode mode = null;

        public ClickOnBoneMode(DisplayMode mode)
        {
            this.mode = mode;
        }

        public override void Update(BaseGame game)
        {
            if (mode.mode != DisplayModeEnum.Bone)
                return;

            if (game.MouseInput.LeftButton == BtnState.Pressing)
                game.Objects.Add(
                    new BoneCreationIntent(game, game
                            .MouseInput
                            .WorldPosition));

        }
    }
}
