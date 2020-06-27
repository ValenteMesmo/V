using Microsoft.Xna.Framework;
using MonogameFacade;

namespace Skeletor
{
    //TODO: girar todos os sprites com as setas no move
    public class SpritePart : GameObject
    {
        private readonly SpriteRenderer sprite;

        public SpritePart(MonogameFacade.Game game)
        {
            this.sprite = new SpriteRenderer();
            sprite.Texture = game.GetTexture("btn");
            sprite.Size = new Point(1000);
            sprite.RotationCenter = new Vector2(
                sprite.Texture.Width / 2,
                sprite.Texture.Height / 2);
            Renderers.Add(sprite);
        }

        public override void Update(MonogameFacade.Game game)
        {
            //sprite.Rotation += 0.01f;
        }
    }
}
