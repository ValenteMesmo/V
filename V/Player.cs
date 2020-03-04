using Microsoft.Xna.Framework;
using MonogameFacade;

namespace V
{
    public class Player : GameObject
    {
        private SpriteRenderer sprite;
        private Collider collider;

        public Player(BaseGame game)
        {
            IsPassive = false;
            sprite = new SpriteRenderer
            {
                Color = Color.Cyan,
                Texture = game.Textures["btn"],
                Size = new Point(100, 100)
            };
            Renderers.Add(sprite);

            collider = new Collider(this);
            collider.Area = new Rectangle(0, 0, 100, 100);
            collider.Handler = new StopsWhenHitingBlocks();
            Colliders.Add(collider);
        }

        public override void Update(BaseGame game)
        {
            Gravity.Apply(this);
        }
    }

    public static class Gravity
    {
        public static void Apply(GameObject obj)
        {
            if (obj.Velocity.Y < 10)
                obj.Velocity.Y = obj.Velocity.Y + 1;
        }
    }
}
