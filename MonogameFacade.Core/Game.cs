using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;

namespace MonogameFacade
{
    using OldGame = Microsoft.Xna.Framework.Game;

    public abstract class Game : OldGame
    {
        public static Game Instance = null;

        public Action<long> Vibrate = null;
        public List<Vector2> Touches = null;
        public List<Vector2> TouchesUi = null;
        private GameObject currentObject = null;
        private Collider currentCollider = null;
        public GraphicsDeviceManager graphics = null;
        private SpriteBatch spriteBatch = null;
        private SpriteBatch spriteBatchGui = null;

        public MouseInput MouseInput = null;

        public readonly Camera Camera = null;
        public readonly Camera GuiCamera = null;

        public SpriteFont Font = null;
        public FrameCounter FrameCounter = null;
        public List<GameObject> Objects = null;

        private Dictionary<string, Texture2D> Textures = null;
        public Texture2D GetTexture(string name)
        {
            return this.Textures[name];
        }

        public Game()
        {
            Instance = this;
            Vibrate = f => { };
            Camera = new Camera();
            GuiCamera = new Camera();
            MouseInput = new MouseInput();
            Camera.Zoom = 0.05f;
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
#if DEBUG
                if (source.Parent == null || target.Parent == null)
                    throw new Exception("Collider parent cannot be null!");
#endif

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
            UpdateTouchsAndClicks();

            for (int i = 0; i < Objects.Count; i++)
            {
                currentObject = Objects[i];
                currentObject.Update();

                currentObject.Location.Y =
                    currentObject.Location.Y
                    + currentObject.Velocity.Y;

                if (!currentObject.IsPassive)
                    for (int j = 0; j < currentObject.Colliders.Count; j++)
                    {
                        currentCollider = currentObject.Colliders[j];

                        //currentCollider.BeforeCollisionHandlers;
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

                        //currentCollider.BeforeCollisionHandlers();
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

        private void UpdateTouchsAndClicks()
        {
            Touches.Clear();
            TouchesUi.Clear();

            var mouse = Mouse.GetState();
            MouseInput.Position =
                GuiCamera.GetWorldPosition(
                        mouse.Position.ToVector2())
                    .ToPoint();

            MouseInput.WorldPosition =
                Camera.GetWorldPosition(
                        mouse.Position.ToVector2())
                    .ToPoint();

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (MouseInput.LeftButton == BtnState.Pressing)
                    MouseInput.LeftButton = BtnState.Pressed;
                else if (MouseInput.LeftButton < BtnState.Pressing)
                    MouseInput.LeftButton = BtnState.Pressing;
            }
            else
            {
                if (MouseInput.LeftButton > BtnState.Pressing)
                    MouseInput.LeftButton = BtnState.Releasing;
                else
                    MouseInput.LeftButton = BtnState.Released;
            }


            if (mouse.LeftButton == ButtonState.Pressed)
            {
                TouchesUi.Add(
                    GuiCamera.GetWorldPosition(
                        mouse.Position.ToVector2())
                    );
                Touches.Add(
                    Camera.GetWorldPosition(
                        mouse.Position.ToVector2()));
            }

            var state = TouchPanel.GetState();
            for (int i = 0; i < state.Count; i++)
                if (state[i].State > 0)
                {
                    TouchesUi.Add(
                        GuiCamera.GetWorldPosition(state[i].Position));
                    Touches.Add(
                        Camera.GetWorldPosition(state[i].Position));
                }
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
