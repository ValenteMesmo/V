using Microsoft.Xna.Framework;
using MonogameFacade.Core.Systems;
using System;

namespace MonogameFacade
{
    public enum DpadDirection
    {
        None, Up, Down, Left, Right
    }

    public class Dpad : GameObject
    {
        private InputKeeper input = null;
        public GuiSpriteRenderer sprite;
        public Rectangle touchArea;
        public Vector2 delta;
        public const int minDistance = 30;
        public const int extraSize = 300;
        public const int minDistanceHalf = 30;
        public DpadDirection CurrentDirection;
        public DpadDirection PreviousDirection;
        public Vector2 DpadCenter;

        public Dpad(BaseGame game, InputKeeper input)
        {
            this.input = input;
            sprite = new GuiSpriteRenderer();
            sprite.Texture = game.Textures["shadedDark04"];
            sprite.Size = new Point(180, 180);
            Location.X = -600;
            Location.Y = -320;
            //sprite.Offset = new Point(-1400, -750);
            //sprite.Offset = new Point(-585, -300);
            //sprite.Offset = new Point(-350, -150);
            Renderers.Add(sprite);
            touchArea = new Rectangle(Location, sprite.Size);
            DpadCenter = touchArea.Center.ToVector2();
            touchArea = new Rectangle(
                Location.X - (extraSize / 2)
                , Location.Y - (extraSize / 2)
                , sprite.Size.X + extraSize
                , sprite.Size.Y + extraSize);
        }

        public override void Update(BaseGame game)
        {
            CalculateDpadDirection(game);

            switch (CurrentDirection)
            {
                case DpadDirection.None:
                    UpdateInput();
                    break;
                case DpadDirection.Up:
                    UpdateInput(up: true);
                    break;
                case DpadDirection.Down:
                    UpdateInput(down: true);
                    break;
                case DpadDirection.Left:
                    UpdateInput(left: true);
                    break;
                case DpadDirection.Right:
                    UpdateInput(right: true);
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

        private void CalculateDpadDirection(BaseGame game)
        {
            switch (CurrentDirection)
            {
                case DpadDirection.Up:
                    sprite.Color = Color.Yellow;
                    break;
                case DpadDirection.Down:
                    sprite.Color = Color.Magenta;
                    break;
                case DpadDirection.Left:
                    sprite.Color = Color.Red;
                    break;
                case DpadDirection.Right:
                    sprite.Color = Color.Blue;
                    break;
                default:
                    sprite.Color = Color.White;
                    break;
            }

            for (int i = 0; i < game.TouchesUi.Count; i++)
            {
                if (touchArea.Contains(game.TouchesUi[i]))
                {
                    delta = DpadCenter - game.TouchesUi[i];
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
                        && distanceYAbs > distanceXAbs
                        && delta.Y < 0;

                    var fingerWentUp =
                        distanceYAbs > minDistance
                        && distanceYAbs > distanceXAbs
                        && delta.Y > 0;

                    if (fingerWentRight)
                    {
                        input.Right = true;
                        input.Right = true;
                        input.Right = true;
                        input.Right = true;
                        CurrentDirection = DpadDirection.Right;
                        DpadCenter = game.TouchesUi[i];
                        DpadCenter.X = DpadCenter.X - minDistance - minDistanceHalf;
                    }
                    else if (fingerWentLeft)
                    {
                        input.Right = true;
                        input.Right = true;
                        input.Right = true;
                        input.Left = true;
                        CurrentDirection = DpadDirection.Left;
                        DpadCenter = game.TouchesUi[i];
                        DpadCenter.X = DpadCenter.X + minDistance + minDistanceHalf;
                    }
                    else if (fingerWentDown)
                    {
                        input.Right = true;
                        input.Right = true;
                        input.Right = true;
                        input.Right = true;
                        CurrentDirection = DpadDirection.Down;
                        DpadCenter = game.TouchesUi[i];
                        DpadCenter.Y = DpadCenter.Y - minDistance - minDistanceHalf;
                    }
                    else if (fingerWentUp)
                    {
                        input.Right = true;
                        input.Right = true;
                        input.Right = true;
                        input.Right = true;
                        CurrentDirection = DpadDirection.Up;
                        DpadCenter = game.TouchesUi[i];
                        DpadCenter.Y = DpadCenter.Y + minDistance + minDistanceHalf;
                    }
                    else
                    {
                        input.Right = true;
                        input.Right = true;
                        input.Right = true;
                        input.Right = true;

                        CurrentDirection = PreviousDirection;
                        return;
                    }

                    PreviousDirection = CurrentDirection;
                    return;
                }
            }
            CurrentDirection = DpadDirection.None;
        }
    }
}
