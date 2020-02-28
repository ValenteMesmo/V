using MonogameFacade;
using System;
using System.Collections.Generic;

namespace V
{
    public class WorldBuilder : GameObject
    {
        private readonly Bag<SpriteRenderer> renderers = new Bag<SpriteRenderer>();

        public WorldBuilder(BaseGame game)
        {
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 10; j++)
                    if (j == 0 || i == 0 || j == 9 || i == 14)
                        AddBlock(game, i, j);
        }

        private void AddBlock(BaseGame game, int i, int j)
        {
            var a = new SpriteRenderer();
            a.Texture = game.Textures["btn"];
            a.Target.X = i * 100;
            a.Target.Y = j * 100;
            a.Target.Width = 100;
            a.Target.Height = 100;
            renderers.Add(a);
        }

        public override void Update(BaseGame game)
        {
            for (int i = 0; i < renderers.Count; i++)
            {
                game.Renderers.Add(renderers[i]);
            }
        }
    }
}
