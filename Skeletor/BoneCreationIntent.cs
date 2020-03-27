using Microsoft.Xna.Framework;
using MonogameFacade;

namespace Skeletor
{
    public class BoneCreationIntent : GameObject
    {
        private SpriteLineRenderer sprite;
        public BoneCreationIntent(BaseGame game, Point offset)
        {
            sprite = new SpriteLineRenderer(game.Textures["btn"], game);
            sprite.end = sprite.start = offset.ToVector2();
            Renderers.Add(sprite);
        }

        public override void Update(BaseGame game)
        {
            //var distance = game.MouseInput.WorldPosition - Location;
            sprite.end = game.MouseInput.WorldPosition.ToVector2();
            if (game.MouseInput.LeftButton == BtnState.Released)
            {
                var actualBone = new BonePart(sprite);
                game.Objects.Add(actualBone);
                game.Objects.Remove(this);
            }
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
}
