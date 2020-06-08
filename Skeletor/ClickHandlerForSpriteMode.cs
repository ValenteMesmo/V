using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameFacade;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Skeletor
{
    public class GuiLabel : GameObject
    {
        private TextRenderer TextRenderer = null;

        public GuiLabel()
        {
            TextRenderer = new TextRenderer();
        }

        public void SetText(string text)
        {
            TextRenderer.Text = text;
        }
    }

    public class ListOfSpritesOnScreen : GameObject
    {
        public List<TextRenderer> texts = null;

        public ListOfSpritesOnScreen(SpriteFont font)
        {
            Location = new Point(-600, -350);
            texts = new List<TextRenderer>();
            texts.AddRange(
                new TextRenderer[]{
                    new TextRenderer(){ Text = "1" ,Font=font , Location = new Point(0,0)}
                    ,new TextRenderer(){ Text = "2",Font=font , Location = new Point(0,150)}
                    ,new TextRenderer(){ Text = "3",Font=font , Location = new Point(0,300)}
                }
            );


        }

        public override void Update(BaseGame game)
        {
            Renderers.Clear();
            Renderers.AddRange(texts);
        }
    }


    public class animsadasd : GameObject
    {
        private Sprite Sprite = null;

        public Vector2 ParentLocation;
        //private ListOfSpritesOnScreen displayMenu = null;

        //public animsadasd(ListOfSpritesOnScreen displayMenu)
        //{
        //    this.displayMenu = displayMenu;
        //}

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
