using Microsoft.Xna.Framework.Input;

namespace MonogameFacade
{
    public class InputKeeper
    {
        public bool Up;
        public bool Down;
        public bool Left;
        public bool Right;
    }


    public static class InputKeeperClear
    {
        public static void Update(InputKeeper input)
        {
            input.Down
                = input.Up
                = input.Left
                = input.Right
                = false;
        }
    }

    public static class KeyboardInput
    {
        public static void Update(InputKeeper direction, InputKeeper action)
        {
            var state = Keyboard.GetState();

            direction.Left = direction.Left 
                || state.IsKeyDown(Keys.A);

            direction.Right = direction.Right 
                || state.IsKeyDown(Keys.D);

            direction.Up = direction.Up 
                || state.IsKeyDown(Keys.W);

            direction.Down = direction.Down 
                || state.IsKeyDown(Keys.S);

            action.Left = action.Left
                || state.IsKeyDown(Keys.J);

            action.Right = action.Right
                || state.IsKeyDown(Keys.L);

            action.Up = action.Up
                || state.IsKeyDown(Keys.I);

            action.Down = action.Down
                || state.IsKeyDown(Keys.K)
                || state.IsKeyDown(Keys.Space);
        }
    }

    public static class GameControllerInput
    {
        private const float thumbStick = 0.45f;
        public static void Update(InputKeeper direction, InputKeeper action)
        {
            var state = GamePad.GetState(0);

            direction.Left = direction.Left
                || state.DPad.Left == ButtonState.Pressed
                || state.ThumbSticks.Left.X < -thumbStick;

            direction.Right = direction.Right
                || state.DPad.Right == ButtonState.Pressed
                || state.ThumbSticks.Left.X > thumbStick;

            direction.Up = direction.Up
                || state.DPad.Up == ButtonState.Pressed
                || state.ThumbSticks.Left.Y > thumbStick;

            direction.Down = direction.Down
                || state.DPad.Down == ButtonState.Pressed
                || state.ThumbSticks.Left.Y < -thumbStick;

            action.Left = action.Left
                || state.Buttons.X == ButtonState.Pressed;

            action.Right = action.Right
                || state.Buttons.B == ButtonState.Pressed;

            action.Up = action.Up
                || state.Buttons.Y == ButtonState.Pressed;

            action.Down = action.Down
                || state.Buttons.A == ButtonState.Pressed;
        }
    }
}
