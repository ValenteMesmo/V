using Microsoft.Xna.Framework;
using MonogameFacade;

namespace V
{
    public static class WorldBuilder
    {
        public static void Create()
        {
            AddBlocks();

            MonogameFacade.Game.Instance.Camera.Location = new Point(6800, 3800);

            var inputAction = new GamePadData { input =new InputKeeper() };
            var inputDirection = new GamePadData { input = new InputKeeper() };

            var player = Player.Create(inputDirection, inputAction);
            player.Location = new Point(1500, 500);

            MonogameFacade.Game.Instance.Objects.Add(Log.Create());

            MonogameFacade.Game.Instance.Objects.Add(DirectionalTouchButtons.Create(inputDirection));
            MonogameFacade.Game.Instance.Objects.Add(ActionTouchButtons.Create(inputAction));
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
            block.Location.X += i * Block.Size;
            block.Location.Y += j * Block.Size;
            MonogameFacade.Game.Instance.Objects.Add(block);
        }
    }
}
