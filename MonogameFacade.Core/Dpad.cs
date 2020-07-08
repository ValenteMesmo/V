using Microsoft.Xna.Framework;
using MonogameFacade.Core.Systems;
using System;

namespace MonogameFacade
{
    public enum GamePadDirection
    {
        None, Up, Down, Left, Right
    }

    public class GamePadData
    {
        public InputKeeper input;
        public Rectangle touchArea;
        public Vector2 delta;
        public GamePadDirection CurrentDirection;
        public GamePadDirection PreviousDirection;
        public Vector2 DpadCenter;
        public Vector2 previousTouch;
        public Color Color;
    }

    public static class BaseTouchButtons
    {
        public const int minDistance = 30;
        public const int extraSize = 150;
        public const int Size = 160;

        public static GameObject Create(GamePadData GamePadData, Point initialLocation)
        {
            var obj = GameObject.GetFromPool();
            obj.Location = initialLocation;
            GamePadData.touchArea = new Rectangle(obj.Location, new Point(Size, Size));
            //GamePadData.DpadCenter = Vector2.Zero;
            //GamePadData.PreviousDirection = GamePadDirection.None;
            //GamePadData.CurrentDirection = GamePadDirection.None;
            GamePadData.previousTouch =
                GamePadData.DpadCenter =
                GamePadData.touchArea.Center.ToVector2();
            GamePadData.Color = Color.White;
            GamePadData.touchArea = new Rectangle(
                obj.Location.X - (extraSize / 2)
                , obj.Location.Y - (extraSize / 2)
                , Size + extraSize
                , Size + extraSize);


            obj.Update = () => Update(GamePadData);
            return obj;
        }

        public static void Update(GamePadData pad)
        {
            var newDirection = CalculateDpadDirection(pad);
            pad.PreviousDirection = pad.CurrentDirection;
            pad.CurrentDirection = newDirection;

            Log.Text = $"{pad.PreviousDirection} : {pad.CurrentDirection}";
            //TODO: introduce cooldown to prevent vibration on release when rapidly pressing
            if (pad.CurrentDirection != pad.PreviousDirection)
                Game.Instance.Vibrate(TouchVibration);

            switch (pad.CurrentDirection)
            {
                case GamePadDirection.None:
                    UpdateInput(pad.input);
                    pad.Color = Color.White;
                    break;
                case GamePadDirection.Up:
                    UpdateInput(pad.input, up: true);
                    pad.Color = Color.Yellow;
                    break;
                case GamePadDirection.Down:
                    UpdateInput(pad.input, down: true);
                    pad.Color = Color.Magenta;
                    break;
                case GamePadDirection.Left:
                    UpdateInput(pad.input, left: true);
                    pad.Color = Color.Red;
                    break;
                case GamePadDirection.Right:
                    UpdateInput(pad.input, right: true);
                    pad.Color = Color.Blue;
                    break;
            }
        }

        private static void UpdateInput(
            InputKeeper input
            , bool up = false
            , bool down = false
            , bool left = false
            , bool right = false)
        {
            input.Up = up;
            input.Down = down;
            input.Left = left;
            input.Right = right;
        }

        private const int TouchVibration = 1;

        private static GamePadDirection CalculateDpadDirection(GamePadData pad)
        {
            for (int i = 0; i < Game.Instance.TouchesUi.Count; i++)
                if (pad.touchArea.Contains(Game.Instance.TouchesUi[i]))
                    if (pad.PreviousDirection == GamePadDirection.Right)
                        return CalculationsFromTheRight(Game.Instance.TouchesUi[i], pad.previousTouch);
                    else if (pad.PreviousDirection == GamePadDirection.Left)
                        return CalculationsFromTheLeft(Game.Instance.TouchesUi[i], pad.previousTouch);
                    else if (pad.PreviousDirection == GamePadDirection.Up)
                        return CalculationsFromUp(Game.Instance.TouchesUi[i], pad.previousTouch);
                    else if (pad.PreviousDirection == GamePadDirection.Down)
                        return CalculationsFromDown(Game.Instance.TouchesUi[i], pad.previousTouch);
                    else
                        return CalculationsFromNone(Game.Instance.TouchesUi[i], pad.previousTouch);

            return GamePadDirection.None;
        }

        private static GamePadDirection CalculationsFromTheRight(Vector2 touch, Vector2 previousTouch)
        {
            var delta = previousTouch - touch;
            var distanceXAbs = Math.Abs(delta.X);
            var distanceYAbs = Math.Abs(delta.Y);

            var fingerWentLeft =
                distanceXAbs > minDistance
                && distanceXAbs > distanceYAbs
                && delta.X > 0;

            var fingerWentDown =
                distanceYAbs > minDistance
                && distanceXAbs < distanceYAbs
                && delta.Y < 0;

            var fingerWentUp =
                distanceYAbs > minDistance
                && distanceXAbs < distanceYAbs
                && delta.Y > 0;

            if (fingerWentLeft)
                if (fingerWentUp)
                    return GamePadDirection.Up;
                else if (fingerWentDown)
                    return GamePadDirection.Down;
                else
                    return GamePadDirection.Left;
            else if (fingerWentUp)
                return GamePadDirection.Up;
            else if (fingerWentDown)
                return GamePadDirection.Down;
            else
                return GamePadDirection.Right;
        }

        private static GamePadDirection CalculationsFromTheLeft(Vector2 touch, Vector2 previousTouch)
        {
            var delta = previousTouch - touch;
            var distanceXAbs = Math.Abs(delta.X);
            var distanceYAbs = Math.Abs(delta.Y);

            var fingerWentRight =
                distanceXAbs > minDistance
                && distanceXAbs > distanceYAbs
                && delta.X < 0;

            var fingerWentDown =
                distanceYAbs > minDistance
                && distanceXAbs < distanceYAbs
                && delta.Y < 0;

            var fingerWentUp =
                distanceYAbs > minDistance
                && distanceXAbs < distanceYAbs
                && delta.Y > 0;

            if (fingerWentRight)
                if (fingerWentUp)
                    return GamePadDirection.Up;
                else if (fingerWentDown)
                    return GamePadDirection.Down;
                else
                    return GamePadDirection.Right;
            else if (fingerWentUp)
                return GamePadDirection.Up;
            else if (fingerWentDown)
                return GamePadDirection.Down;
            else
                return GamePadDirection.Left;
        }

        private static GamePadDirection CalculationsFromUp(Vector2 touch, Vector2 previousTouch)
        {
            var delta = previousTouch - touch;
            var distanceXAbs = Math.Abs(delta.X);
            var distanceYAbs = Math.Abs(delta.Y);

            var fingerWentRight =
                distanceXAbs > minDistance
                && distanceXAbs > distanceYAbs
                && delta.X < 0;

            var fingerWentLeft =
                distanceXAbs > minDistance
                && distanceXAbs > distanceYAbs
                && delta.X > 0;

            var fingerWentDown =
                distanceYAbs > minDistance
                && distanceXAbs < distanceYAbs
                && delta.Y < 0;

            if (fingerWentDown)
                if (fingerWentRight)
                    return GamePadDirection.Right;
                else if (fingerWentLeft)
                    return GamePadDirection.Left;
                else
                    return GamePadDirection.Down;
            else if (fingerWentLeft)
                return GamePadDirection.Left;
            else if (fingerWentRight)
                return GamePadDirection.Right;
            else
                return GamePadDirection.Up;
        }

        private static GamePadDirection CalculationsFromDown(Vector2 touch, Vector2 previousTouch)
        {
            var delta = previousTouch - touch;
            var distanceXAbs = Math.Abs(delta.X);
            var distanceYAbs = Math.Abs(delta.Y);

            var fingerWentRight =
                distanceXAbs > minDistance
                && distanceXAbs > distanceYAbs
                && delta.X < 0;

            var fingerWentLeft =
                distanceXAbs > minDistance
                && distanceXAbs > distanceYAbs
                && delta.X > 0;

            var fingerWentUp =
                distanceYAbs > minDistance
                && distanceXAbs < distanceYAbs
                && delta.Y > 0;

            if (fingerWentUp)
                if (fingerWentRight)
                    return GamePadDirection.Right;
                else if (fingerWentLeft)
                    return GamePadDirection.Left;
                else
                    return GamePadDirection.Up;
            else if (fingerWentRight)
                return GamePadDirection.Right;
            else if (fingerWentLeft)
                return GamePadDirection.Left;
            else
                return GamePadDirection.Down;
        }

        private static GamePadDirection CalculationsFromNone(Vector2 touch, Vector2 previousTouch)
        {
            var delta = previousTouch - touch;
            var distanceXAbs = Math.Abs(delta.X);
            var distanceYAbs = Math.Abs(delta.Y);

            var fingerWentRight =
                distanceXAbs > minDistance
                && distanceXAbs > distanceYAbs
                && delta.X < 0;

            var fingerWentLeft =
                distanceXAbs > minDistance
                && distanceXAbs > distanceYAbs
                && delta.X > 0;

            var fingerWentDown =
                distanceYAbs > minDistance
                && distanceXAbs < distanceYAbs
                && delta.Y < 0;

            var fingerWentUp =
                distanceYAbs > minDistance
                && distanceXAbs < distanceYAbs
                && delta.Y > 0;

            if (fingerWentRight)
                if (fingerWentUp)
                    return GamePadDirection.Up;
                else if (fingerWentDown)
                    return GamePadDirection.Down;
                else
                    return GamePadDirection.Right;
            else if (fingerWentLeft)
                if (fingerWentUp)
                    return GamePadDirection.Up;
                else if (fingerWentDown)
                    return GamePadDirection.Down;
                else
                    return GamePadDirection.Left;
            else if (fingerWentUp)
                return GamePadDirection.Up;
            else if (fingerWentDown)
                return GamePadDirection.Down;
            else
                return GamePadDirection.None;
        }
    }

    public static class DirectionalTouchButtons
    {
        public static GameObject Create(InputKeeper input)
        {
            var data = new GamePadData
            {
                input = input
            };

            var obj = BaseTouchButtons.Create(data, new Point(-600, -320));

            var sprite2 = GuiSpriteRenderer.GetFromPool();
            sprite2.Texture = Game.Instance.GetTexture("btn");
            sprite2.Size = new Point(BaseTouchButtons.Size, BaseTouchButtons.Size);
            sprite2.Color = Color.DarkGray;
            //obj.Renderers.Add(sprite2);


            var sprite = GuiSpriteRenderer.GetFromPool();
            sprite.Texture = Game.Instance.GetTexture("shadedDark04");
            sprite.Size = new Point(BaseTouchButtons.Size, BaseTouchButtons.Size);
            //sprite.Offset = new Point(-1400, -750);
            //sprite.Offset = new Point(-585, -300);
            //sprite.Offset = new Point(-350, -150);
            obj.Renderers.Add(sprite);

            data.touchArea = new Rectangle(obj.Location, sprite.Size); ;
            data.previousTouch = data.DpadCenter = data.touchArea.Center.ToVector2();
            data.touchArea = new Rectangle(
                obj.Location.X - (BaseTouchButtons.extraSize / 2)
                , obj.Location.Y - (BaseTouchButtons.extraSize / 2)
                , sprite.Size.X + BaseTouchButtons.extraSize
                , sprite.Size.Y + BaseTouchButtons.extraSize);

            sprite2.Size = data.touchArea.Size;
            sprite2.Offset = new Point(-(BaseTouchButtons.extraSize / 2), -(BaseTouchButtons.extraSize / 2));

            return obj;
        }


    }

    public static class ActionTouchButtons
    {
        const int buttonSize = 70;

        public static GameObject Create(InputKeeper input)
        {
            var data = new GamePadData
            {
                input = input
            };
            var obj = BaseTouchButtons.Create(data, new Point(430, -320));

            var sprite = GuiSpriteRenderer.GetFromPool();
            sprite.Texture = Game.Instance.GetTexture("btn");
            sprite.Size = new Point(BaseTouchButtons.Size, BaseTouchButtons.Size);
            sprite.Color = Color.DarkGray;
            //Renderers.Add(sprite);

            var sprite1 = GuiSpriteRenderer.GetFromPool();
            sprite1.Texture = Game.Instance.GetTexture("shadedDark39");
            sprite1.Offset = new Point(45, -10);
            sprite1.Size = new Point(buttonSize, buttonSize);
            obj.Renderers.Add(sprite1);

            var sprite2 = GuiSpriteRenderer.GetFromPool();
            sprite2.Texture = Game.Instance.GetTexture("shadedDark37");
            sprite2.Offset = new Point(100, 45);
            sprite2.Size = new Point(buttonSize, buttonSize);
            obj. Renderers.Add(sprite2);

            var sprite3 = GuiSpriteRenderer.GetFromPool();
            sprite3.Texture = Game.Instance.GetTexture("shadedDark36");
            sprite3.Offset = new Point(45, 100);
            sprite3.Size = new Point(buttonSize, buttonSize);
            obj.Renderers.Add(sprite3);

            var sprite4 = GuiSpriteRenderer.GetFromPool();
            sprite4.Texture = Game.Instance.GetTexture("shadedDark38");
            sprite4.Offset = new Point(-10, 45);
            sprite4.Size = new Point(buttonSize, buttonSize);
            obj. Renderers.Add(sprite4);

            data.touchArea = new Rectangle(obj.Location, new Point(BaseTouchButtons.Size, BaseTouchButtons.Size));
            data.previousTouch = data.DpadCenter = data.touchArea.Center.ToVector2();
            data.touchArea = new Rectangle(
                obj.Location.X - (BaseTouchButtons.extraSize / 2)
                , obj.Location.Y - (BaseTouchButtons.extraSize / 2)
                , BaseTouchButtons.Size + BaseTouchButtons.extraSize
                , BaseTouchButtons.Size + BaseTouchButtons.extraSize);

            sprite.Size = data.touchArea.Size;
            sprite.Offset = new Point(-(BaseTouchButtons.extraSize / 2), -(BaseTouchButtons.extraSize / 2));
            return obj;
        }
    }
}
