using Microsoft.Xna.Framework;
using MonogameFacade;
using System;
using System.Collections.Generic;

namespace V
{
    public class WorldBuilder : GameObject
    {
        public WorldBuilder(MonogameFacade.Game game)
        {
            Location = new Point(-1500, -1500);
            AddBlocks(game);
            var player = new Player(game) { Location = new Point(200, 200) };
            game.Objects.Add(new Dpad(game, player.inputTouch));
            game.Objects.Add(new ActionButtons(game, player.inputTouchAction));
            game.Objects.Add(player);
            game.Objects.Add(new FpsDisplay(game));
        }

        private void AddBlocks(MonogameFacade.Game game)
        {
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 10; j++)
                    if (j == 0 || i == 0 || j == 9 || i == 14)
                        AddBlock(game, i, j);
        }

        private void AddBlock(MonogameFacade.Game game, int i, int j)
        {
            var block = new Block(game);
            block.Location = Location;
            block.Location.X = block.Location.X + i * Block.Size;
            block.Location.Y = block.Location.Y + j * Block.Size;
            game.Objects.Add(block);
        }

        public override void Update(MonogameFacade.Game game)
        {
        }
    }
}
