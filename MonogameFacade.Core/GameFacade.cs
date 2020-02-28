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
        //TODO: move to here all these facade properties
        public SpriteFont Font { get => GameFacade.font; }
        public FrameCounter FrameCounter { get => GameFacade.FrameCounter; }
        public Bag<Renderer> Renderers { get => GameFacade.Renderers; }
        public Bag<GameObject> Objects { get => GameFacade.Objects; }
        public Dictionary<string, Texture2D> Textures { get => GameFacade.Texutes; }


        public BaseGame()
        {
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

    public class GameFacade : Game
    {
        public FrameCounter FrameCounter = new FrameCounter();
        public Bag<GameObject> Objects = new Bag<GameObject>();
        public Bag<Renderer> Renderers = new Bag<Renderer>();
        public Bag<Vector2> Touches = new Bag<Vector2>();
        private readonly BaseGame BaseGame;
        public readonly Camera Camera;

        public GraphicsDeviceManager graphics;
        public Dictionary<string, Texture2D> Texutes { get; private set; }

        private SpriteBatch spriteBatch;
        public SpriteFont font;

        public GameFacade(BaseGame BaseGame)
        {
            this.BaseGame = BaseGame;
            this.Camera = new Camera();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("font");
            Texutes = BaseGame.LoadTextures(Content);

            BaseGame.Initialize();
            BaseGame.Ready(BaseGame);
        }

        public void ActualUpdate(GameTime gameTime)
        {
            Renderers.Clear();

            for (int i = 0; i < Objects.Count; i++)
                Objects[i].Update(BaseGame);

            Camera.Update(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            BaseGame.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            BaseGame.Draw(gameTime);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                Camera.transform);

            for (var i = 0; i < Renderers.Count; i++)
                Renderers[i].Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
