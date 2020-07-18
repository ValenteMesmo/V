using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

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
        public Point delta;
        public GamePadDirection CurrentDirection;
        public GamePadDirection PreviousDirection;
        public GamePadDirection PreviousDirectionNotNone;
        public Color Color;
    }

    public static class BaseTouchButtons
    {
        public const int minDistance = 30;
        public const int extraSize = 80;
        public const int Size = 160;

        public static GameObject Create(GamePadData GamePadData, Point initialLocation)
        {
            var obj = GameObject.GetFromPool();
            obj.Location = initialLocation;
            GamePadData.touchArea = new Rectangle(obj.Location, new Point(Size, Size));
            GamePadData.Color = Color.White;
            GamePadData.touchArea = new Rectangle(
                obj.Location.X - (extraSize / 2)
                , obj.Location.Y - (extraSize / 2)
                , Size + extraSize
                , Size + extraSize);

            return obj;
        }

        public static void Update(GamePadData pad)
        {
            if (pad.CurrentDirection != GamePadDirection.None)
                pad.PreviousDirectionNotNone = pad.CurrentDirection;

            pad.PreviousDirection = pad.CurrentDirection;

            pad.CurrentDirection = CalculateDpadDirection(pad, Game.Instance.TouchesUi);

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
            input.Up = input.Up || up;
            input.Down = input.Down || down;
            input.Left = input.Left || left;
            input.Right = input.Right || right;
        }

        private const int TouchVibration = 1;

        public const int minVerticalOnSlide = 15;
        public const int minVerticalOnPress = 33;
        public const int minHorizontalOnSlide = 15;
        public const int minHorizontalOnPress = 33;

        private static GamePadDirection CalculateDpadDirection(GamePadData pad, List<Point> TouchesUi)
        {
            for (int i = 0; i < TouchesUi.Count; i++)
                if (pad.touchArea.Contains(TouchesUi[i]))
                {
                    var delta = pad.touchArea.Center - TouchesUi[i];

                    var left = delta.X > 0 ? Math.Abs(delta.X) : 0;
                    var right = delta.X < 0 ? Math.Abs(delta.X) : 0;
                    var up = delta.Y > 0 ? Math.Abs(delta.Y) : 0;
                    var down = delta.Y < 0 ? Math.Abs(delta.Y) : 0;

                    if (pad.PreviousDirection == GamePadDirection.None)
                    {
                        if (left < minHorizontalOnSlide)
                            left = 0;

                        if (right < minHorizontalOnSlide)
                            right = 0;

                        if (up < minVerticalOnSlide)
                            up = 0;

                        if (down < minVerticalOnSlide)
                            down = 0;
                    }
                    else
                    {
                        if (left < minHorizontalOnPress)
                            left = 0;

                        if (right < minHorizontalOnPress)
                            right = 0;

                        if (up < minVerticalOnPress)
                            up = 0;

                        if (down < minVerticalOnPress)
                            down = 0;
                    }

                    if (pad.PreviousDirection == GamePadDirection.Right)
                        return CalculateSlideFromTheRight(pad.PreviousDirectionNotNone, left, right, up, down);
                    else if (pad.PreviousDirection == GamePadDirection.Left)
                        return CalculateSlideFromTheLeft(pad.PreviousDirectionNotNone,left, right, up, down);
                    else if (pad.PreviousDirection == GamePadDirection.Up)
                        return CalculateSlideFromUp(pad.PreviousDirectionNotNone,left, right, up, down);
                    else if (pad.PreviousDirection == GamePadDirection.Down)
                        return CalculateSlideFromDown(pad.PreviousDirectionNotNone,left, right, up, down);
                    else
                        return CalculatePressStart(pad.PreviousDirectionNotNone, left, right, up, down);
                }

            return GamePadDirection.None;
        }

        private static GamePadDirection CalculateSlideFromTheRight(GamePadDirection PreviousDirectionNotNone, int left, int right, int up, int down)
        {
            if (up > 0 && right > 0)
                return PreviousDirectionNotNone;

            if (down > 0 && right > 0)
                return PreviousDirectionNotNone;

            if (up > 0)
                return GamePadDirection.Up;
            if (down > 0)
                return GamePadDirection.Down;
            if (left > 0)
                return GamePadDirection.Left;
            return GamePadDirection.Right;
        }

        private static GamePadDirection CalculateSlideFromTheLeft(GamePadDirection PreviousDirectionNotNone, int left, int right, int up, int down)
        {
            if (up > 0 && left > 0)
                return PreviousDirectionNotNone;

            if (down > 0 && left > 0)
                return PreviousDirectionNotNone;

            if (up > 0)
                return GamePadDirection.Up;
            if (down > 0)
                return GamePadDirection.Down;
            if (right > 0)
                return GamePadDirection.Right;
            return GamePadDirection.Left;
        }

        private static GamePadDirection CalculateSlideFromUp(GamePadDirection PreviousDirectionNotNone, int left, int right, int up, int down)
        {
            if (left > 0 && up > 0)
                return PreviousDirectionNotNone;

            if (right > 0 && up > 0 )
                return PreviousDirectionNotNone;

            if (left > right)
                return GamePadDirection.Left;

            if (right > left)
                return GamePadDirection.Right;

            if (down > 0)
                return GamePadDirection.Down;

            return GamePadDirection.Up;
        }

        private static GamePadDirection CalculateSlideFromDown(GamePadDirection PreviousDirectionNotNone, int left, int right, int up, int down)
        {
            if (left > 0 && down > 0)
                return PreviousDirectionNotNone;

            if (right > 0 && down > 0)
                return PreviousDirectionNotNone;

            if (left > right)
                return GamePadDirection.Left;

            if (right > left)
                return GamePadDirection.Right;

            if (up > 0)
                return GamePadDirection.Up;

            return GamePadDirection.Down;
        }

        private static GamePadDirection CalculatePressStart(GamePadDirection PreviousDirectionNotNone, int left, int right, int up, int down)
        {
            if (PreviousDirectionNotNone == GamePadDirection.Right)
                if (left > 0 && left > up && left > down)
                    return GamePadDirection.Left;
                else if (up > 0)
                    return GamePadDirection.Up;
                else if (down > 0)
                    return GamePadDirection.Down;
                else
                    return GamePadDirection.Right;

            else if (PreviousDirectionNotNone == GamePadDirection.Left)
                if (right > 0 && right > up && right > down)
                    return GamePadDirection.Right;
                else if (up > 0)
                    return GamePadDirection.Up;
                else if (down > 0)
                    return GamePadDirection.Down;
                else
                    return GamePadDirection.Left;

            else if (PreviousDirectionNotNone == GamePadDirection.Down)
                if (up > 0 && up > left && up > right)
                    return GamePadDirection.Up;
                else if (left > 0)
                    return GamePadDirection.Left;
                else if (right > 0)
                    return GamePadDirection.Right;
                else
                    return GamePadDirection.Down;

            else if (PreviousDirectionNotNone == GamePadDirection.Up)
                if (down > 0 && down > left && down > right)
                    return GamePadDirection.Down;
                else if (left > 0)
                    return GamePadDirection.Left;
                else if (right > 0)
                    return GamePadDirection.Right;
                else
                    return GamePadDirection.Up;

            else
            {
                if (up > 0 && up > left && up > right && up > down)
                    return GamePadDirection.Up;
                else if (down > 0 && down > left && down > right && down > up)
                    return GamePadDirection.Down;
                else if (left > 0 && left > right && left > up && left > down)
                    return GamePadDirection.Left;
                else
                    return GamePadDirection.Right;
            }
        }
    }

    public static class DirectionalTouchButtons
    {
        public static GameObject Create(GamePadData data)
        {
            var obj = BaseTouchButtons.Create(data, new Point(-600, -320));

            var sprite2 = GuiSpriteRenderer.GetFromPool();
            sprite2.Texture = Game.Instance.GetTexture("btn");
            sprite2.Size = new Point(BaseTouchButtons.Size, BaseTouchButtons.Size);
            sprite2.Color = Color.DarkGray;
            obj.Renderers.Add(sprite2);

            var sprite = GuiSpriteRenderer.GetFromPool();
            sprite.Texture = Game.Instance.GetTexture("shadedDark04");
            sprite.Size = new Point(BaseTouchButtons.Size, BaseTouchButtons.Size);
            obj.Renderers.Add(sprite);

            data.touchArea = new Rectangle(obj.Location, sprite.Size); ;
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

        public static GameObject Create(GamePadData data)
        {
            var obj = BaseTouchButtons.Create(data, new Point(440, -320));

            var sprite = GuiSpriteRenderer.GetFromPool();
            sprite.Texture = Game.Instance.GetTexture("btn");
            sprite.Size = new Point(BaseTouchButtons.Size, BaseTouchButtons.Size);
            sprite.Color = Color.DarkGray;
            obj.Renderers.Add(sprite);

            var sprite1 = GuiSpriteRenderer.GetFromPool();
            sprite1.Texture = Game.Instance.GetTexture("shadedDark39");
            sprite1.Offset = new Point(45, -10);
            sprite1.Size = new Point(buttonSize, buttonSize);
            obj.Renderers.Add(sprite1);

            var sprite2 = GuiSpriteRenderer.GetFromPool();
            sprite2.Texture = Game.Instance.GetTexture("shadedDark37");
            sprite2.Offset = new Point(100, 45);
            sprite2.Size = new Point(buttonSize, buttonSize);
            obj.Renderers.Add(sprite2);

            var sprite3 = GuiSpriteRenderer.GetFromPool();
            sprite3.Texture = Game.Instance.GetTexture("shadedDark36");
            sprite3.Offset = new Point(45, 100);
            sprite3.Size = new Point(buttonSize, buttonSize);
            obj.Renderers.Add(sprite3);

            var sprite4 = GuiSpriteRenderer.GetFromPool();
            sprite4.Texture = Game.Instance.GetTexture("shadedDark38");
            sprite4.Offset = new Point(-10, 45);
            sprite4.Size = new Point(buttonSize, buttonSize);
            obj.Renderers.Add(sprite4);

            data.touchArea = new Rectangle(obj.Location, new Point(BaseTouchButtons.Size, BaseTouchButtons.Size));
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
