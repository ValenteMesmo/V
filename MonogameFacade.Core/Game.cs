using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;

using OldGame = Microsoft.Xna.Framework.Game;

namespace MonogameFacade
{
    public interface IGame
    {
        Action<long> Vibrate { get; }
        Texture2D GetTexture(string name);
        double CurrentFramesPerSecond { get; }
        SpriteFont Font { get; }
        List<GameObject> Objects { get; }
        Camera Camera { get; }
        Camera GuiCamera { get; }

        List<Point> TouchesUi { get; }
    }

    public abstract class Game : OldGame, IGame
    {
        private const double frameRate = 0.01666666666;
        private double accumulator;

        public static IGame Instance = null;

        public Action<long> Vibrate { get; set; }
        public List<Point> Touches { get; set; }
        public List<Point> TouchesUi { get; set; }
        private GameObject currentObject = null;
        private Collider currentCollider = null;
        public GraphicsDeviceManager graphics = null;
        private SpriteBatch spriteBatch = null;
        private SpriteBatch spriteBatchGui = null;

        public MouseInput MouseInput = null;

        public Camera Camera { get;  }
        public Camera GuiCamera { get; }

        public SpriteFont Font { get; set; }
        private Texture2D pixel;
        public FrameCounter FrameCounter = null;
        public double CurrentFramesPerSecond => FrameCounter.CurrentFramesPerSecond;
        public List<GameObject> Objects { get; }

        private Dictionary<string, Texture2D> Textures = null;

        private DateTime previousUpdate;
        private DateTime currentUpdate;
        private DateTime actualCurrentUpdate;
        private double delta;

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
            Touches = new List<Point>();
            TouchesUi = new List<Point>();

            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            InactiveSleepTime = new TimeSpan(0);
        }

        protected abstract Dictionary<string, Texture2D> LoadTextures(ContentManager Content);

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatchGui = new SpriteBatch(GraphicsDevice); ;

            Font = Content.Load<SpriteFont>("font");
            Textures = LoadTextures(Content);
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            pixel.Dispose();
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

        private void ActualUpdate()
        {
            UpdateTouchsAndClicks();

            for (int i = 0; i < Objects.Count; i++)
            {
                currentObject = Objects[i];
                currentObject.Update();

                currentObject.Location.Y += currentObject.Velocity.Y;

                if (!currentObject.IsPassive)
                    for (int j = 0; j < currentObject.Colliders.Count; j++)
                    {
                        currentCollider = currentObject.Colliders[j];

                        currentCollider.BeforeCollisionHandler();
                        for (int k = 0; k < Objects.Count; k++)
                            for (int l = 0; l < Objects[k].Colliders.Count; l++)
                                CheckCollisions(
                                    CollisionDirection.Vertical
                                    , currentCollider
                                    , Objects[k].Colliders[l]);
                    }

                currentObject.Location.X += currentObject.Velocity.X;
                if (!currentObject.IsPassive)
                    for (int j = 0; j < currentObject.Colliders.Count; j++)
                    {
                        currentCollider = currentObject.Colliders[j];

                        currentCollider.BeforeCollisionHandler();
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

        protected override void Update(GameTime gameTime)
        {
            previousUpdate = currentUpdate;
            currentUpdate = DateTime.Now;
            delta = (currentUpdate - previousUpdate).TotalSeconds;
            if (delta > 0.25)
                delta = 0.25;

            accumulator += delta;

            if (accumulator >= frameRate)
            {
                FrameCounter.Update((currentUpdate - actualCurrentUpdate).TotalSeconds);
                actualCurrentUpdate = currentUpdate;

                while (accumulator >= frameRate)
                {
                    ActualUpdate();
                    accumulator -= frameRate;
                }
            }
            else
                SuppressDraw();
        }

        private void UpdateTouchsAndClicks()
        {
            Touches.Clear();
            TouchesUi.Clear();

            var mouse = Mouse.GetState();
            MouseInput.Position =
                GuiCamera.GetWorldPosition(mouse.Position);

            MouseInput.WorldPosition =
                Camera.GetWorldPosition(mouse.Position);

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
                    GuiCamera.GetWorldPosition(mouse.Position)
                    );
                Touches.Add(Camera.GetWorldPosition(mouse.Position));
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

            for (int i = 0; i < Objects.Count; i++)
                for (var j = 0; j < Objects[i].Renderers.Count; j++)
                    for (int k = 0; k < Objects[i].Colliders.Count; k++)
                        DrawBorder(Objects[i].Colliders[k].RelativeArea, 20, Color.Red, spriteBatch);

            spriteBatch.End();
            spriteBatchGui.End();
            base.Draw(gameTime);
        }

        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder), rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder, rectangleToDraw.Width, thicknessOfBorder), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
