using Microsoft.Xna.Framework;
using MonogameFacade;
using MonogameFacade.Core.Systems;

namespace V
{
    public class Player : GameObject
    {
        private SpriteRenderer sprite = null;
        private Collider collider = null;
        public InputKeeper input = null;
        public InputKeeper inputTouch = null;
        public InputKeeper inputTouchAction = null;
        public Player(BaseGame game)
        {
            input = new InputKeeper();
            inputTouch = new InputKeeper();
            inputTouchAction = new InputKeeper();
            IsPassive = false;
            sprite = new SpriteRenderer
            {
                Color = Color.Cyan,
                Texture = game.GetTexture("btn"],
                Size = new Point(1000, 1000)
            };
            Renderers.Add(sprite);

            collider = new Collider(this);
            collider.Area = new Rectangle(Point.Zero, sprite.Size);
            collider.Handler = new StopsWhenHitingBlocks();
            Colliders.Add(collider);
        }

        public override void Update(BaseGame game)
        {
            SetInputUsingKeyboard.Update(input);
            MovesUsingKeyboard.Update(this, input, inputTouch);
            JumpsUsingInput.Update(this, inputTouchAction);
            Gravity.Apply(this);
        }
    }
}
