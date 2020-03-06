using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MonogameFacade
{
    public abstract class BaseGame : IDisposable
    {
        protected readonly GameFacade GameFacade = null;
        public readonly Camera Camera = null;
        public readonly Camera GuiCamera = null;

        public SpriteFont Font = null;
        public FrameCounter FrameCounter = null;
        public List<GameObject> Objects = null;

        public Dictionary<string, Texture2D> Textures = null;

        public BaseGame()
        {
            this.Camera = new Camera();
            this.GuiCamera = new Camera() { Location = Point.Zero };
            Camera.Zoom = 0.05f;
            GuiCamera.Zoom = 1f;

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
