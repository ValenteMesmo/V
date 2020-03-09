using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameFacade.Core.Systems
{
    public class InputKeeper
    {
        public bool Up;
        public bool Down;
        public bool Left;
        public bool Right;
    }

    public static class SetInputUsingKeyboard
    {
        public static void Update(InputKeeper inputs)
        {
            var state = Keyboard.GetState();

            inputs.Left = state.IsKeyDown(Keys.A);
            inputs.Right = state.IsKeyDown(Keys.D);
            inputs.Up = state.IsKeyDown(Keys.W);
            inputs.Down = state.IsKeyDown(Keys.S);
        }
    }

    public static class MovesUsingKeyboard
    {
        public static void Update(
            GameObject obj
            , InputKeeper inputs
            , InputKeeper Touchinputs)
        {
            if (inputs.Left || Touchinputs.Left)
                obj.Velocity.X = -90;
            else if (inputs.Right || Touchinputs.Right)
                obj.Velocity.X = 90;
            else
                obj.Velocity.X = 0;
        }
    }
}
