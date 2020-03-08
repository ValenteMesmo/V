using Microsoft.Xna.Framework;

namespace MonogameFacade
{
    public class Block : GameObject
    {
        public Block(BaseGame game)
        {
            var sprite = new SpriteRenderer();
            sprite.Texture = game.Textures["btn"];
            sprite.Size = new Point(100, 100);
            Renderers.Add(sprite);

            var collider = new Collider(this);
            collider.Area = new Rectangle(Point.Zero, sprite.Size);
            Colliders.Add(collider);
        }

    }
}
