using Microsoft.Xna.Framework;
using MonogameFacade;
using MonogameFacade.Core.Systems;

namespace V
{
    public static class Player
    {
        public static GameObject Create(
            GamePadData inputDirection
            , GamePadData inputAction
        )
        {
            var obj = GameObject.GetFromPool();
            obj.IsPassive = false;
            obj.Identifier = Identifier.Player;
            var sprite = SpriteRenderer.GetFromPool();

            sprite.Color = Color.Cyan;
            sprite.Texture = MonogameFacade.Game.Instance.GetTexture("btn");
            sprite.Size = new Point(1000, 1000);

            obj.Renderers.Add(sprite);

            var collider = Collider.GetFromPool();
            collider.Area = new Rectangle(Point.Zero, sprite.Size);
            collider.Parent = obj;
            collider.BotCollisionHandler = StopsWhenHitingBlocks.Bot;
            collider.TopCollisionHandler = StopsWhenHitingBlocks.Top;
            collider.LeftCollisionHandler = StopsWhenHitingBlocks.Left;
            collider.RightCollisionHandler = StopsWhenHitingBlocks.Right;
            obj.Colliders.Add(collider);

            var grounded = false;

            var groundCheck = Collider.GetFromPool();
            groundCheck.Parent = obj;
            groundCheck.Area = new Rectangle(0, sprite.Size.Y, sprite.Size.X, 200);

            groundCheck.BeforeCollisionHandler = () => grounded = false;
            groundCheck.BotCollisionHandler =
                groundCheck.TopCollisionHandler =
                groundCheck.LeftCollisionHandler =
                groundCheck.RightCollisionHandler =
                (source, target) =>
                {
                    if (target.Parent.Identifier == Identifier.Block)
                        grounded = true;
                };
            obj.Colliders.Add(groundCheck);

            obj.Update = () =>
            {
                InputKeeperClear.Update(inputDirection.input);
                InputKeeperClear.Update(inputAction.input);

                BaseTouchButtons.Update(inputAction);
                BaseTouchButtons.Update(inputDirection);
                KeyboardInput.Update(inputDirection.input, inputAction.input);
                GameControllerInput.Update(inputDirection.input, inputAction.input);

                HorizontalMovement.Update(obj, inputDirection.input);
                JumpsUsingInput.Update(obj, inputAction.input, grounded);
                Gravity.Apply(obj);
            };

            return obj;
        }
    }
}
