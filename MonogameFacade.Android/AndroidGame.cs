using Android.Content.Res;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.IO;

namespace MonogameFacade
{
    public abstract class AndroidGame : BaseGame
    {
        private readonly AssetManager assets;

        public GameServiceContainer Services { get => GameFacade.Services; }

        public AndroidGame(AssetManager assets):base()
        {
            this.assets = assets;            
        }
        
        public override void Initialize()
        {
            GameFacade.graphics.IsFullScreen = true;
            GameFacade.graphics.PreferredBackBufferFormat = SurfaceFormat.HdrBlendable;
            GameFacade.graphics.GraphicsProfile = GraphicsProfile.HiDef;
            GameFacade.graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
            GameFacade.graphics.ApplyChanges();
        }

        public override void Update(GameTime gameTime)
        {
            var state = TouchPanel.GetState();
            GameFacade.Touches.Clear();

            for (int i = 0; i < state.Count; i++)
                if (state[i].State > 0)
                    GameFacade.Touches.Add(
                        GameFacade.Camera.GetWorldPosition(state[i].Position));

            GameFacade.ActualUpdate(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            FrameCounter.Update(
                gameTime.ElapsedGameTime.TotalSeconds);
        }

        public override Dictionary<string, Texture2D> LoadTextures(ContentManager content)
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
