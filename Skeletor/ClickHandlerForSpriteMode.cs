using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameFacade;
using System;

namespace Skeletor
{
    public class animsadasd : GameObject
    {
        private Sprite Sprite;

        public Vector2 ParentLocation;

    

        internal void AddSprite(Sprite sprite)
        {
            if (Sprite == null)
            {
                Sprite = sprite;
                Renderers.Add(Sprite);
                ParentLocation = Sprite.Location;
            }
            else
            {
                sprite.Location = Vector2.Transform(
                    sprite.Location
                    , Matrix.Invert(Sprite.LocalTransform)
                );
                Sprite.Children.Add(sprite);
                ParentLocation = Sprite.Location;
            }
        }
    }

    public class ClickHandlerForSpriteMode : GameObject
    {
        private readonly DisplayMode mode = null;
        private readonly animsadasd Animation;
        private readonly Sprite preview;

        public ClickHandlerForSpriteMode(
            DisplayMode mode,
            animsadasd Animation,
            Texture2D texutre
        )
        {
            this.mode = mode;
            this.Animation = Animation;
            this.preview = new Sprite(texutre) { };
        }

        public override void Update(BaseGame game)
        {
            Renderers.Clear();
            if (mode.mode != DisplayModeEnum.Sprite)
                return;

            Renderers.Add(preview);
            preview.Location = game.MouseInput.WorldPosition.ToVector2() 
                //+ Animation.ParentLocation
                ;
            if (game.MouseInput.LeftButton == BtnState.Pressing)
            {
                var sprite = new Sprite(game.GetTexture("btn"));
                sprite.Location = preview.Location;
                Animation.AddSprite(sprite);
            }
        }
    }
}
