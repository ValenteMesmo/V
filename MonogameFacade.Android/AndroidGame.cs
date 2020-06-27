using Android.Content.Res;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.IO;

namespace MonogameFacade
{
    public abstract class AndroidGame : Game
    {
        private readonly AssetManager assets = null;

        public AndroidGame(AssetManager assets) : base()
        {
            this.assets = assets;
        }

        protected override void Draw(GameTime gameTime)
        {
            FrameCounter.Update(
                gameTime.ElapsedGameTime.TotalSeconds);

            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferFormat = SurfaceFormat.HdrBlendable;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
            graphics.ApplyChanges();
        }

        protected override Dictionary<string, Texture2D> LoadTextures(ContentManager content)
        {
            var result = new Dictionary<string, Texture2D>();

            var textures = assets.List("Content/Textures");

            foreach (var texture in textures)
            {
                var key = Path.GetFileNameWithoutExtension(texture);
                result.Add(key, content.Load<Texture2D>($"Textures/{key}"));
            }

            return result;
        }
    }
}
