using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MonogameFacade
{
    public abstract class BaseGame : IDisposable
    {
        protected readonly GameFacade GameFacade;

        public SpriteFont Font;
        public FrameCounter FrameCounter = null;
        public List<GameObject> Objects = null;

        public Dictionary<string, Texture2D> Textures = null;

        public BaseGame()
        {
            Objects = new List<GameObject>();
            FrameCounter = new FrameCounter();
            GameFacade = new GameFacade(this);
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Initialize();
        public abstract void Draw(GameTime gameTime);
        public abstract void Ready(BaseGame game);

        public abstract Dictionary<string, Texture2D> LoadTextures(ContentManager content);

        public void Run()
        {
            GameFacade.Run();
        }

        public void Dispose()
        {
            GameFacade.Dispose();
        }
    }
}
