using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonogameFacade
{
    public class GameFacade : Game
    {
        private readonly BaseGame BaseGame = null;
        public List<Vector2> Touches = null;
        private GameObject currentObject = null;
        private Collider currentCollider = null;
        public GraphicsDeviceManager graphics = null;
        private SpriteBatch spriteBatch = null;
        private SpriteBatch spriteBatchGui = null;

        public GameFacade(BaseGame BaseGame)
        {
            this.BaseGame = BaseGame;
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            Touches = new List<Vector2>();
        }

        protected override void Initialize()
        {
        
            base.Initialize();

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatchGui = new SpriteBatch(GraphicsDevice); ;

            BaseGame.Font = Content.Load<SpriteFont>("font");
            BaseGame.Textures = BaseGame.LoadTextures(Content);

            BaseGame.Ready(BaseGame);
            BaseGame.Initialize();
        }


        public void ActualUpdate(GameTime gameTime)
        {
            for (int i = 0; i < BaseGame.Objects.Count; i++)
            {
                currentObject = BaseGame.Objects[i];
                currentObject.Update(BaseGame);

                currentObject.Location.Y =
                    currentObject.Location.Y
                    + currentObject.Velocity.Y;

                if (!currentObject.IsPassive)
                    for (int j = 0; j < currentObject.Colliders.Count; j++)
                    {
                        currentCollider = currentObject.Colliders[j];

                        currentCollider.Handler.BeforeCollisions();
                        for (int k = 0; k < BaseGame.Objects.Count; k++)
                            for (int l = 0; l < BaseGame.Objects[k].Colliders.Count; l++)
                                CheckCollisions(CollisionDirection.Vertical, currentCollider, BaseGame.Objects[k].Colliders[l]);
                    }

                currentObject.Location.X =
                    currentObject.Location.X
                    + currentObject.Velocity.X;
                if (!currentObject.IsPassive)
                    for (int j = 0; j < currentObject.Colliders.Count; j++)
                    {
                        currentCollider = currentObject.Colliders[j];

                        currentCollider.Handler.BeforeCollisions();
                        for (int k = 0; k < BaseGame.Objects.Count; k++)
                            for (int l = 0; l < BaseGame.Objects[k].Colliders.Count; l++)
                                CheckCollisions(CollisionDirection.Horizontal, currentCollider, BaseGame.Objects[k].Colliders[l]);
                    }
            }

            BaseGame.Camera.Update(GraphicsDevice);
            BaseGame.GuiCamera.Update(GraphicsDevice);
        }

        private void CheckCollisions(CollisionDirection direction, Collider source, Collider target)
        {
            //var targets = quadtree.Get(source);

            //for (int i = 0; i < targets.Length; i++)
            {
                if (source.Parent == target.Parent)
                    return;

                if (direction == CollisionDirection.Vertical)
                    source.IsCollidingV(target);
                else
                    source.IsCollidingH(target);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            BaseGame.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            BaseGame.Draw(gameTime);
            GraphicsDevice.Clear(Color.Black);

            spriteBatchGui.Begin(
                SpriteSortMode.Deferred
                //, BlendState.AlphaBlend
                , BlendState.NonPremultiplied
                , SamplerState.PointClamp
                , DepthStencilState.Default
                , RasterizerState.CullNone
                , null
                , BaseGame.GuiCamera.transform
            );

            spriteBatch.Begin(
               SpriteSortMode.Deferred
                //, BlendState.AlphaBlend
                , BlendState.NonPremultiplied
                , SamplerState.PointClamp
                , DepthStencilState.Default
                , RasterizerState.CullNone
                , null
                , BaseGame.Camera.transform
            );

            for (int i = 0; i < BaseGame.Objects.Count; i++)
                for (var j = 0; j < BaseGame.Objects[i].Renderers.Count; j++)
                    BaseGame
                        .Objects[i]
                        .Renderers[j]
                        .Draw(
                            spriteBatchGui
                            , spriteBatch
                            , BaseGame.Objects[i]);

            spriteBatch.End();
            spriteBatchGui.End();
            base.Draw(gameTime);
        }
    }
}
