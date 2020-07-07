using Microsoft.Xna.Framework;
using MonogameFacade;
using MonogameFacade.Core.Systems;

namespace V
{
    public static class Player
    {
        //private SpriteRenderer sprite = null;
        //private Collider collider = null;
        //public InputKeeper input ;
        //public InputKeeper inputTouch ;
        //public InputKeeper inputTouchAction ;

        public static GameObject Create(
            InputKeeper input
            , InputKeeper inputTouch
            , InputKeeper inputTouchAction
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

            obj.Update = () => Update(obj, input, inputTouch, inputTouchAction);

            return obj;
        }

        public static void Update(
            GameObject obj
            , InputKeeper input
            , InputKeeper inputTouch
            , InputKeeper inputTouchAction)
        {
            SetInputUsingKeyboard.Update(input);
            MovesUsingKeyboard.Update(obj, input, inputTouch);
            JumpsUsingInput.Update(obj, inputTouchAction);
            Gravity.Apply(obj);
        }
    }
}
