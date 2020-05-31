using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameFacade;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Skeletor
{
    public class ListOfSpritesOnScreen : GameObject
    {
        public List<TextRenderer> texts = null;

        public ListOfSpritesOnScreen(SpriteFont font)
        {
            Location = new Point(-600,-350);
            texts = new List<TextRenderer>();
            texts.AddRange(
                new TextRenderer[]{
                    new TextRenderer(){ Text = "1" ,Font=font , Location = new Point(0,0)}
                    ,new TextRenderer(){ Text = "2",Font=font , Location = new Point(0,150)}
                    ,new TextRenderer(){ Text = "3",Font=font , Location = new Point(0,300)}
                }
            );

            Renderers.AddRange(texts);
        }

        public override void Update(BaseGame game)
        {

        }
    }


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
