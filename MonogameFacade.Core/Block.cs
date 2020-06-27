using Microsoft.Xna.Framework;

namespace MonogameFacade
{
    public static class Block 
    {
        public const int Size = 1000;

        public static GameObject Create() {

            var obj = GameObject.GetFromPool();

            var sprite = new SpriteRenderer();
            sprite.Texture = game.GetTexture("btn");
            sprite.Size = new Point(Size, Size);
            Renderers.Add(sprite);

            var collider = new Collider(this);
            collider.Area = new Rectangle(Point.Zero, sprite.Size);
            Colliders.Add(collider);

            return obj;
        }
    }
}
