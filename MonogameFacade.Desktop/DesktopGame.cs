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

        public override void Draw(GameTime gameTime)
        {
        }

        public override void Initialize()
        {
            GameFacade.IsMouseVisible = true;
            GameFacade.IsFixedTimeStep = false;
            GameFacade.graphics.SynchronizeWithVerticalRetrace = false;
            GameFacade.InactiveSleepTime = new TimeSpan(0);
            GameFacade.graphics.PreferredBackBufferFormat = SurfaceFormat.HdrBlendable;
            //GameFacade.graphics.GraphicsProfile = GraphicsProfile.HiDef;
            GameFacade.graphics.ApplyChanges();
        }

        public override Dictionary<string, Texture2D> LoadTextures(ContentManager content)
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
        public override void Update(GameTime gameTime)
        {
            //currentUpdate = DateTime.Now;
            //delta = (currentUpdate - previousUpdate).TotalSeconds;
            //if (delta > 0.25)
            //    delta = 0.25;
            //previousUpdate = currentUpdate;

            //accumulator = accumulator + delta;
            accumulator = accumulator + gameTime.ElapsedGameTime.TotalSeconds;

            GameFacade.Touches.Clear();
            var mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
                GameFacade.Touches.Add(
                    GameFacade.Camera.GetWorldPosition(
                        mouse.Position.ToVector2()));

            if (accumulator >= dt)
            {
                GameFacade.ActualUpdate(gameTime);
                accumulator = accumulator - dt;
            }
            else
                GameFacade.SuppressDraw();

            FrameCounter.Update(accumulator);
        }
    }
}
