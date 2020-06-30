using Microsoft.Xna.Framework;

namespace MonogameFacade
{
    public static class Block
    {
        public const int Size = 1000;

        public static GameObject Create()
        {
            var obj = GameObject.GetFromPool();

            var sprite = SpriteRenderer.GetFromPool();
            sprite.Texture = Game.Instance.GetTexture("btn");
            sprite.Size = new Point(Size, Size);
            obj.Renderers.Add(sprite);

            var collider = Collider.GetFromPool();
            collider.Area = new Rectangle(Point.Zero, sprite.Size);
            obj.Colliders.Add(collider);

            return obj;
        }
    }
}
