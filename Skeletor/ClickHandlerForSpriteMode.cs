using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameFacade;

namespace Skeletor
{
    public class animsadasd : GameObject
    {
        public SpriteOnDrawMode Sprite = null;
        public SharedProperty<int> CurrentDepth = null;
        public Vector2 ParentLocation;
        //private ListOfSpritesOnScreen displayMenu = null;

        //public animsadasd(ListOfSpritesOnScreen displayMenu)
        //{
        //    this.displayMenu = displayMenu;
        //}


        public animsadasd()
        {
            CurrentDepth = new SharedProperty<int>();
        }
        internal void AddSprite(SpriteOnDrawMode sprite)
        {
            if (Sprite == null)
            {
                Sprite = sprite;
                Renderers.Add(Sprite);
                ParentLocation = Sprite.Location;
            }
            else
            {
                CurrentDepth.Value = sprite.OwnDepth = Sprite.OwnDepth + 1;
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

        public override void Update(MonogameFacade.Game game)
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
                var sprite = new SpriteOnDrawMode(game.GetTexture("btn"), Animation.CurrentDepth);
                sprite.Location = preview.Location;
                Animation.AddSprite(sprite);
            }
        }
    }
}
