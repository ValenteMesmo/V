using Microsoft.Xna.Framework;
using MonogameFacade.Core.Systems;
using System;

namespace MonogameFacade
{
    public enum GamePadDirection
    {
        None, Up, Down, Left, Right
    }

    public struct GamePadData
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

    public static class BaseFourTouchanleButtons
    {
        public const int minDistance = 30;
        public const int extraSize = 150;
        public const int Size = 160;

        public static GameObject Create(GamePadData GamePadData)
        {
            var obj = GameObject.GetFromPool();

            //GamePadData.touchArea = new Rectangle(Location, new Point(Size, Size));
            //GamePadData.DpadCenter = Vector2.Zero;
            //GamePadData.PreviousDirection = GamePadDirection.None;
            //GamePadData.CurrentDirection = GamePadDirection.None;
            //GamePadData.previousTouch =
            //    GamePadData.DpadCenter =
            //    GamePadData.touchArea.Center.ToVector2();
            GamePadData.Color = Color.White;
            //GamePadData.touchArea = new Rectangle(
            //    Location.X - (extraSize / 2)
            //    , Location.Y - (extraSize / 2)
            //    , Size + extraSize
            //    , Size + extraSize);


            obj.Update = () => Update(GamePadData);
            return obj;
        }

        public static void Update(GamePadData pad)
        {
            var newDirection = CalculateDpadDirection(pad);
            pad.PreviousDirection = pad.CurrentDirection;
            pad.CurrentDirection = newDirection;

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
        private const int UntouchVibration = 1;

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

    public static class Dpad
    {
        //public GuiSpriteRenderer sprite = null;

        public static GameObject Create(InputKeeper input)
        {
            var data = new GamePadData();
            data.input = input;

            var obj = BaseFourTouchanleButtons.Create(data);
            obj.Location = new Point(-600, -320);

            var sprite2 = new GuiSpriteRenderer();
            sprite2.Texture = Game.Instance.GetTexture("btn");
            sprite2.Size = new Point(BaseFourTouchanleButtons.Size, BaseFourTouchanleButtons.Size);
            sprite2.Color = Color.DarkGray;
            //Renderers.Add(sprite2);


            var sprite = new GuiSpriteRenderer();
            sprite.Texture = Game.Instance.GetTexture("shadedDark04");
            sprite.Size = new Point(BaseFourTouchanleButtons.Size, BaseFourTouchanleButtons.Size);
            //sprite.Offset = new Point(-1400, -750);
            //sprite.Offset = new Point(-585, -300);
            //sprite.Offset = new Point(-350, -150);
            obj.Renderers.Add(sprite);

            data.touchArea = new Rectangle(obj.Location, sprite.Size); ;
            data.previousTouch = data.DpadCenter = data.touchArea.Center.ToVector2();
            data.touchArea = new Rectangle(
                obj.Location.X - (BaseFourTouchanleButtons.extraSize / 2)
                , obj.Location.Y - (BaseFourTouchanleButtons.extraSize / 2)
                , sprite.Size.X + BaseFourTouchanleButtons.extraSize
                , sprite.Size.Y + BaseFourTouchanleButtons.extraSize);

            sprite2.Size = data.touchArea.Size;
            sprite2.Offset = new Point(-(BaseFourTouchanleButtons.extraSize / 2), -(BaseFourTouchanleButtons.extraSize / 2));

            return obj;
        }


    }

    public static class ActionButtons
    {
        const int buttonSize = 70;

        public static GameObject Create(InputKeeper input)
        {
            var data = new GamePadData();
            data.input = input;
            var obj = BaseFourTouchanleButtons.Create(data);

            obj.Location = new Point(430, -320);

            var sprite = new GuiSpriteRenderer();
            sprite.Texture = Game.Instance.GetTexture("btn");
            sprite.Size = new Point(BaseFourTouchanleButtons.Size, BaseFourTouchanleButtons.Size);
            sprite.Color = Color.DarkGray;
            //Renderers.Add(sprite);

            var sprite1 = new GuiSpriteRenderer();
            sprite1.Texture = Game.Instance.GetTexture("shadedDark39");
            sprite1.Offset = new Point(45, -10);
            sprite1.Size = new Point(buttonSize, buttonSize);
            obj.Renderers.Add(sprite1);

            var sprite2 = new GuiSpriteRenderer();
            sprite2.Texture = Game.Instance.GetTexture("shadedDark37");
            sprite2.Offset = new Point(100, 45);
            sprite2.Size = new Point(buttonSize, buttonSize);
            obj. Renderers.Add(sprite2);

            var sprite3 = new GuiSpriteRenderer();
            sprite3.Texture = Game.Instance.GetTexture("shadedDark36");
            sprite3.Offset = new Point(45, 100);
            sprite3.Size = new Point(buttonSize, buttonSize);
            obj.Renderers.Add(sprite3);

            var sprite4 = new GuiSpriteRenderer();
            sprite4.Texture = Game.Instance.GetTexture("shadedDark38");
            sprite4.Offset = new Point(-10, 45);
            sprite4.Size = new Point(buttonSize, buttonSize);
            obj. Renderers.Add(sprite4);

            data.touchArea = new Rectangle(obj.Location, new Point(BaseFourTouchanleButtons.Size, BaseFourTouchanleButtons.Size));
            data.previousTouch = data.DpadCenter = data.touchArea.Center.ToVector2();
            data.touchArea = new Rectangle(
                obj.Location.X - (BaseFourTouchanleButtons.extraSize / 2)
                , obj.Location.Y - (BaseFourTouchanleButtons.extraSize / 2)
                , BaseFourTouchanleButtons.Size + BaseFourTouchanleButtons.extraSize
                , BaseFourTouchanleButtons.Size + BaseFourTouchanleButtons.extraSize);

            sprite.Size = data.touchArea.Size;
            sprite.Offset = new Point(-(BaseFourTouchanleButtons.extraSize / 2), -(BaseFourTouchanleButtons.extraSize / 2));
            return obj;
        }
    }
}
