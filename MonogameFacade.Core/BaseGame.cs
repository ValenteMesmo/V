using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MonogameFacade
{
    public abstract class BaseGame : Game
    {
        public Action<long> Vibrate = f => { };
        public List<Vector2> Touches = null;
        public List<Vector2> TouchesUi = null;
        private GameObject currentObject = null;
        private Collider currentCollider = null;
        public GraphicsDeviceManager graphics = null;
        private SpriteBatch spriteBatch = null;
        private SpriteBatch spriteBatchGui = null;

        public readonly Camera Camera = null;
        public readonly Camera GuiCamera = null;

        public SpriteFont Font = null;
        public FrameCounter FrameCounter = null;
        public List<GameObject> Objects = null;

        public Dictionary<string, Texture2D> Textures = null;

        public BaseGame()
        {
            this.Camera = new Camera();
            this.GuiCamera = new Camera() ;
            Camera.Zoom = 0.04f;
            GuiCamera.Zoom = 1f;

            Objects = new List<GameObject>();
            FrameCounter = new FrameCounter();

            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            Touches = new List<Vector2>();
            TouchesUi = new List<Vector2>();
        }

        protected abstract Dictionary<string, Texture2D> LoadTextures(ContentManager Content);

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatchGui = new SpriteBatch(GraphicsDevice); ;

            Font = Content.Load<SpriteFont>("font");
            Textures = LoadTextures(Content);
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
            for (int i = 0; i < Objects.Count; i++)
            {
                currentObject = Objects[i];
                currentObject.Update(this);

                currentObject.Location.Y =
                    currentObject.Location.Y
                    + currentObject.Velocity.Y;

                if (!currentObject.IsPassive)
                    for (int j = 0; j < currentObject.Colliders.Count; j++)
                    {
                        currentCollider = currentObject.Colliders[j];

                        currentCollider.Handler.BeforeCollisions();
                        for (int k = 0; k < Objects.Count; k++)
                            for (int l = 0; l < Objects[k].Colliders.Count; l++)
                                CheckCollisions(
                                    CollisionDirection.Vertical
                                    , currentCollider
                                    , Objects[k].Colliders[l]);
                    }

                currentObject.Location.X =
                    currentObject.Location.X
                    + currentObject.Velocity.X;
                if (!currentObject.IsPassive)
                    for (int j = 0; j < currentObject.Colliders.Count; j++)
                    {
                        currentCollider = currentObject.Colliders[j];

                        currentCollider.Handler.BeforeCollisions();
                        for (int k = 0; k < Objects.Count; k++)
                            for (int l = 0; l < Objects[k].Colliders.Count; l++)
                                CheckCollisions(
                                    CollisionDirection.Horizontal
                                    , currentCollider
                                    , Objects[k].Colliders[l]);
                    }
            }

            //Camera.Update(GraphicsDevice);
            //GuiCamera.Update(GraphicsDevice);
        }

        protected override void Draw(GameTime gameTime)
        {            
            GraphicsDevice.Clear(Color.Black);

            spriteBatchGui.Begin(
                SpriteSortMode.Deferred
                , BlendState.NonPremultiplied
                , SamplerState.PointClamp
                , DepthStencilState.Default
                , RasterizerState.CullNone
                , null
                , GuiCamera.GetTransformation(GraphicsDevice)
            );

            spriteBatch.Begin(
               SpriteSortMode.Deferred
                , BlendState.NonPremultiplied
                , SamplerState.PointClamp
                , DepthStencilState.Default
                , RasterizerState.CullNone
                , null
                , Camera.GetTransformation(GraphicsDevice)
            );

            for (int i = 0; i < Objects.Count; i++)
                for (var j = 0; j < Objects[i].Renderers.Count; j++)                    
                    Objects[i]
                        .Renderers[j]
                        .Draw(
                            spriteBatchGui
                            , spriteBatch
                            , Objects[i]);

            spriteBatch.End();
            spriteBatchGui.End();
            base.Draw(gameTime);
        }
    }
}
