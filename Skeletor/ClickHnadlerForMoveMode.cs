using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameFacade;
using System;
using System.Collections.Generic;

namespace Skeletor
{ 
    public class ClickHnadlerForMoveMode : GameObject
    {
        private readonly DisplayMode mode = null;

        public ClickHnadlerForMoveMode(
            DisplayMode mode
            , BaseGame game)
        {
            this.mode = mode;
        }

        public override void Update(BaseGame game)
        {
            if (mode.mode != DisplayModeEnum.Move)
                return;

        }
    }
}
