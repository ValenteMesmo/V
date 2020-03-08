using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameFacade.Core.Systems
{
    public static class MovesUsingKeyboard
    {
        public static void Update(BaseGame game, GameObject obj)
        {
            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.A))
                obj.Velocity.X = -6;
            else if (state.IsKeyDown(Keys.D))
                obj.Velocity.X = 6;
            else
                obj.Velocity.X = 0;
        }
    }
}
