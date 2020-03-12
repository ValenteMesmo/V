using Microsoft.Xna.Framework;
using MonogameFacade.Core.Systems;
using System;

namespace MonogameFacade
{
    public enum DpadDirection
    {
        None, Up, Down, Left, Right
    }

    public abstract class BaseFourTouchanleButtons : GameObject
    {
        protected InputKeeper input = null;
        public Rectangle touchArea;
        public Vector2 delta;
        public const int minDistance = 30;
        public const int extraSize = 150;
        public const int Size = 160;
        public DpadDirection CurrentDirection;
        public DpadDirection PreviousDirection;
        public Vector2 DpadCenter;
        public Vector2 previousTouch;
        protected Color Color;

        public BaseFourTouchanleButtons(BaseGame game, InputKeeper input)
        {
            this.input = input;
            touchArea = new Rectangle(Location, new Point(Size, Size));
            previousTouch = DpadCenter = touchArea.Center.ToVector2();
            touchArea = new Rectangle(
                Location.X - (extraSize / 2)
                , Location.Y - (extraSize / 2)
                , Size + extraSize
                , Size + extraSize);
        }

        public override void Update(BaseGame game)
        {
            var newDirection = CalculateDpadDirection(game);
            PreviousDirection = CurrentDirection;
            CurrentDirection = newDirection;

            if (CurrentDirection != PreviousDirection)
                game.Vibrate(TouchVibration);

            switch (CurrentDirection)
            {
                case DpadDirection.None:
                    UpdateInput();
                    Color = Color.White;
                    break;
                case DpadDirection.Up:
                    UpdateInput(up: true);
                    Color = Color.Yellow;
                    break;
                case DpadDirection.Down:
                    UpdateInput(down: true);
                    Color = Color.Magenta;
                    break;
                case DpadDirection.Left:
                    UpdateInput(left: true);
                    Color = Color.Red;
                    break;
                case DpadDirection.Right:
                    UpdateInput(right: true);
                    Color = Color.Blue;
                    break;
            }
        }

        private void UpdateInput(
            bool up = false
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

        private DpadDirection CalculateDpadDirection(BaseGame game)
        {
            for (int i = 0; i < game.TouchesUi.Count; i++)
                if (touchArea.Contains(game.TouchesUi[i]))
                    if (PreviousDirection == DpadDirection.Right)
                        return CalculationsFromTheRight(game.TouchesUi[i]);
                    else if (PreviousDirection == DpadDirection.Left)
                        return CalculationsFromTheLeft(game.TouchesUi[i]);
                    else if (PreviousDirection == DpadDirection.Up)
                        return CalculationsFromUp(game.TouchesUi[i]);
                    else if (PreviousDirection == DpadDirection.Down)
                        return CalculationsFromDown(game.TouchesUi[i]);
                    else
                        return CalculationsFromNone(game.TouchesUi[i]);

            return DpadDirection.None;
        }

        private DpadDirection CalculationsFromTheRight(Vector2 touch)
        {
            delta = previousTouch - touch;
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
                    return DpadDirection.Up;
                else if (fingerWentDown)
                    return DpadDirection.Down;
                else
                    return DpadDirection.Left;
            else if (fingerWentUp)
                return DpadDirection.Up;
            else if (fingerWentDown)
                return DpadDirection.Down;
            else
                return DpadDirection.Right;
        }

        private DpadDirection CalculationsFromTheLeft(Vector2 touch)
        {
            delta = previousTouch - touch;
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
                    return DpadDirection.Up;
                else if (fingerWentDown)
                    return DpadDirection.Down;
                else
                    return DpadDirection.Right;
            else if (fingerWentUp)
                return DpadDirection.Up;
            else if (fingerWentDown)
                return DpadDirection.Down;
            else
                return DpadDirection.Left;
        }

        private DpadDirection CalculationsFromUp(Vector2 touch)
        {
            delta = previousTouch - touch;
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
                    return CurrentDirection = DpadDirection.Right;
                else if (fingerWentLeft)
                    return CurrentDirection = DpadDirection.Left;
                else
                    return DpadDirection.Down;
            else if (fingerWentLeft)
                return DpadDirection.Left;
            else if (fingerWentRight)
                return DpadDirection.Right;
            else
                return DpadDirection.Up;
        }

        private DpadDirection CalculationsFromDown(Vector2 touch)
        {
            delta = previousTouch - touch;
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
                    return DpadDirection.Right;
                else if (fingerWentLeft)
                    return DpadDirection.Left;
                else
                    return DpadDirection.Up;
            else if (fingerWentRight)
                return DpadDirection.Right;
            else if (fingerWentLeft)
                return DpadDirection.Left;
            else
                return DpadDirection.Down;
        }

        private DpadDirection CalculationsFromNone(Vector2 touch)
        {
            delta = previousTouch - touch;
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
                    return DpadDirection.Up;
                else if (fingerWentDown)
                    return DpadDirection.Down;
                else
                    return DpadDirection.Right;
            else if (fingerWentLeft)
                if (fingerWentUp)
                    return DpadDirection.Up;
                else if (fingerWentDown)
                    return DpadDirection.Down;
                else
                    return DpadDirection.Left;
            else if (fingerWentUp)
                return DpadDirection.Up;
            else if (fingerWentDown)
                return DpadDirection.Down;
            else
                return DpadDirection.None;
        }
    }

    public class Dpad : BaseFourTouchanleButtons
    {
        public GuiSpriteRenderer sprite = null;

        public Dpad(BaseGame game, InputKeeper input) : base(game, input)
        {
            var sprite2 = new GuiSpriteRenderer();
            sprite2.Texture = game.Textures["btn"];
            sprite2.Size = new Point(Size, Size);
            sprite2.Color = Color.DarkGray;
            //Renderers.Add(sprite2);


            sprite = new GuiSpriteRenderer();
            sprite.Texture = game.Textures["shadedDark04"];
            sprite.Size = new Point(Size, Size);
            Location.X = -600;
            Location.Y = -320;
            //sprite.Offset = new Point(-1400, -750);
            //sprite.Offset = new Point(-585, -300);
            //sprite.Offset = new Point(-350, -150);
            Renderers.Add(sprite);
            touchArea = new Rectangle(Location, sprite.Size);
            previousTouch = DpadCenter = touchArea.Center.ToVector2();
            touchArea = new Rectangle(
                Location.X - (extraSize / 2)
                , Location.Y - (extraSize / 2)
                , sprite.Size.X + extraSize
                , sprite.Size.Y + extraSize);

            sprite2.Size = touchArea.Size;
            sprite2.Offset = new Point(-(extraSize / 2), -(extraSize / 2));
        }


    }

    public class ActionButtons : BaseFourTouchanleButtons
    {
        const int buttonSize = 70;

        public ActionButtons(BaseGame game, InputKeeper input) : base(game, input)
        {
            Location.X = 430;
            Location.Y = -320;

            var sprite = new GuiSpriteRenderer();
            sprite.Texture = game.Textures["btn"];
            sprite.Size = new Point(Size, Size);
            sprite.Color = Color.DarkGray;
            //Renderers.Add(sprite);

            var sprite1 = new GuiSpriteRenderer();
            sprite1.Texture = game.Textures["shadedDark39"];
            sprite1.Offset = new Point(45, -10);
            sprite1.Size = new Point(buttonSize, buttonSize);
            Renderers.Add(sprite1);

            var sprite2 = new GuiSpriteRenderer();
            sprite2.Texture = game.Textures["shadedDark37"];
            sprite2.Offset = new Point(100, 45);
            sprite2.Size = new Point(buttonSize, buttonSize);
            Renderers.Add(sprite2);

            var sprite3 = new GuiSpriteRenderer();
            sprite3.Texture = game.Textures["shadedDark36"];
            sprite3.Offset = new Point(45, 100);
            sprite3.Size = new Point(buttonSize, buttonSize);
            Renderers.Add(sprite3);

            var sprite4 = new GuiSpriteRenderer();
            sprite4.Texture = game.Textures["shadedDark38"];
            sprite4.Offset = new Point(-10, 45);
            sprite4.Size = new Point(buttonSize, buttonSize);
            Renderers.Add(sprite4);

            touchArea = new Rectangle(Location, new Point(Size, Size));
            previousTouch = DpadCenter = touchArea.Center.ToVector2();
            touchArea = new Rectangle(
                Location.X - (extraSize / 2)
                , Location.Y - (extraSize / 2)
                , Size + extraSize
                , Size + extraSize);

            sprite.Size = touchArea.Size;
            sprite.Offset = new Point(-(extraSize / 2), -(extraSize / 2));
        }
    }
}
