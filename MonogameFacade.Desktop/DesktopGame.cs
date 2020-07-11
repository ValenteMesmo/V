using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace MonogameFacade
{
    public abstract class DesktopGame : Game
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
            //10240;
            //Camera.windowWidth;
            graphics.PreferredBackBufferHeight =
                664;
            //4320;
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
    }
}
