using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace MonogameFacade
{
    public abstract class DesktopGame : BaseGame
    {
        const double dt = 0.0166;
        double accumulator;

        //DateTime previousUpdate = DateTime.Now;

        public DesktopGame() : base() { }        

        protected override void Initialize()
        {
            base.Initialize();
            //GameFacade.graphics.IsFullScreen = true;
            IsMouseVisible = true;
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            InactiveSleepTime = new TimeSpan(0);
            graphics.PreferredBackBufferFormat = SurfaceFormat.HdrBlendable;
            //GameFacade.graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferredBackBufferWidth =
                1176;
            //Camera.windowWidth;
            graphics.PreferredBackBufferHeight =
                664;
                //Camera.windowHeight;
            graphics.ApplyChanges();
        }

        protected override Dictionary<string, Texture2D> LoadTextures(ContentManager content)
        {
            var result = new Dictionary<string, Texture2D>();

            var files = Directory.GetFiles("Content/Textures");
            foreach (var file in files)
            {
                var key = Path.GetFileNameWithoutExtension(file);
                var path = $"Textures/{key}";
                result.Add(key, content.Load<Texture2D>(path));
            }

            return result;
        }
        //DateTime currentUpdate;
        //double delta;
        protected override void Update(GameTime gameTime)
        {
            //currentUpdate = DateTime.Now;
            //delta = (currentUpdate - previousUpdate).TotalSeconds;
            //if (delta > 0.25)
            //    delta = 0.25;
            //previousUpdate = currentUpdate;

            //accumulator = accumulator + delta;
            accumulator = accumulator + gameTime.ElapsedGameTime.TotalSeconds;

            Touches.Clear();
            TouchesUi.Clear();
            var mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                TouchesUi.Add(
                    GuiCamera.GetWorldPosition(
                        mouse.Position.ToVector2()));
                Touches.Add(
                    Camera.GetWorldPosition(
                        mouse.Position.ToVector2()));
            }

            if (accumulator >= dt)
            {
                base.Update(gameTime);
                accumulator = accumulator - dt;
            }
            else
                SuppressDraw();

            FrameCounter.Update(accumulator);
        }
    }
}
