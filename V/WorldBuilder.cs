using Microsoft.Xna.Framework;
using MonogameFacade;
using MonogameFacade.Core.Systems;
using System;
using System.Collections.Generic;

namespace V
{
    public static class WorldBuilder
    {
        public static void Create()
        {
            AddBlocks();

            MonogameFacade.Game.Instance.Camera.Location = new Point(6800,3800);

            var inputTouch = new InputKeeper();
            var inputTouchAction = new InputKeeper();
            var input = new InputKeeper();

            var player = Player.Create(input, inputTouch, inputTouchAction);
            player.Location = new Point(1500, 500);
            MonogameFacade.Game.Instance.Objects.Add(Dpad.Create(inputTouch));
            MonogameFacade.Game.Instance.Objects.Add(ActionButtons.Create(inputTouchAction));
            MonogameFacade.Game.Instance.Objects.Add(player);
            MonogameFacade.Game.Instance.Objects.Add(FpsDisplay.Create());
        }

        private static void AddBlocks()
        {
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 10; j++)
                    if (j == 0 || i == 0 || j == 9 || i == 14)
                        AddBlock(i, j);
        }

        private static void AddBlock(int i, int j)
        {
            var block = Block.Create();            
            block.Location.X = block.Location.X + i * Block.Size;
            block.Location.Y = block.Location.Y + j * Block.Size;
            MonogameFacade.Game.Instance.Objects.Add(block);
        }
    }
}
