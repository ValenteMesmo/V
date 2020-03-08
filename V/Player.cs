using Microsoft.Xna.Framework;
using MonogameFacade;
using MonogameFacade.Core.Systems;

namespace V
{
    public class Player : GameObject
    {
        private SpriteRenderer sprite = null;
        private Collider collider = null;

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
            MovesUsingKeyboard.Update(game, this);
            Gravity.Apply(this);
        }
    }
}
