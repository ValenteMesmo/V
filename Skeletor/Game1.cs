using Microsoft.Xna.Framework.Graphics;
using MonogameFacade;

namespace Skeletor
{
    public class SpriteRotationTest : GameObject
    {
        private Sprite main;
        private Sprite sub;

        public SpriteRotationTest(Texture2D texture)
        {
            this. main = new Sprite(texture)
            {
                Scale = new Microsoft.Xna.Framework.Vector2(2, 2)
                
            };
            this.sub = new Sprite(texture)
            {
                Scale = new Microsoft.Xna.Framework.Vector2(1, 1)
            };

            main.Children.Add(sub);
            Renderers.Add(main);
        }

        public override void Update(BaseGame game)
        {
            sub.Rotation += 0.01f;
            main.Rotation += 0.01f;
        }
    }
    public class Game1 : DesktopGame
    {
        protected override void LoadContent()
        {
            base.LoadContent();

            //var texture = this.GetTexture("block");w

            //Objects.Add(new SpriteRotationTest(texture)
            //{
            //    Location = new Microsoft.Xna.Framework.Point(100, 100)
            //});
            var mode = new DisplayMode(this);
            //var skeleton = new SkeletonAnimationParts();
            //var boneHandler = new ClickHandlerForBoneEndMode(mode, skeleton);
            Objects.Add(mode);
            var animsaidasd = new animsadasd();
            Objects.Add(animsaidasd);
            Objects.Add(new ClickHandlerForSpriteMode(mode, animsaidasd, GetTexture("btn")));
            //Objects.Add(new ClickHandlerForBoneStartMode(mode, boneHandler, skeleton));
            //Objects.Add(boneHandler);
            Objects.Add(new ClickHnadlerForMoveMode(mode, this));

            Objects.Add(new ListOfSpritesOnScreen(Font));
        }
    }
}
