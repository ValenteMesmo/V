using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameFacade;

namespace Skeletor
{
    public class SpriteRotationTest
    {
        public static GameObject Create(Texture2D texture)
        {
            var obj = GameObject.GetFromPool();

            var main = SpriteHierarchyRenderer.GetFromPool();
            main.Texture = texture;
            main.Scale = new Microsoft.Xna.Framework.Vector2(24, 24);

            var sub = SpriteHierarchyRenderer.GetFromPool();
            sub.Texture = texture;
            sub.Scale = new Microsoft.Xna.Framework.Vector2(1, 1);

            main.Children.Add(sub);
            obj.Renderers.Add(main);

            obj.Update = () => Update(main, sub);

            return obj;
        }

        public static void Update(SpriteHierarchyRenderer main, SpriteHierarchyRenderer sub)
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

            //var mode = new DisplayMode(this);
            //Objects.Add(mode);
            //var animsaidasd = new animsadasd();
            //Objects.Add(animsaidasd);
            //Objects.Add(new ClickHandlerForSpriteMode(mode, animsaidasd, GetTexture("btn")));
            //Objects.Add(new ClickHnadlerForMoveMode(mode, this));

            //Objects.Add(new ListOfSpritesOnScreen(animsaidasd));
            //Objects.Add(SpriteRotationTest.Create(GetTexture("btn")));
            Objects.Add(Stritesuhaduwhduehfuehfuehgfur.Create());
        }
    }

    public static class Stritesuhaduwhduehfuehfuehgfur
    {
        public static GameObject Create()
        {
            var obj = GameObject.GetFromPool();

            SpriteHierarchyRenderer main = null;

            obj.Update = () =>
            {
                if (MonogameFacade.Game.Instance.MouseInput.LeftButton != BtnState.Pressing)
                    return;

                var mousePosition = MonogameFacade.Game.Instance.MouseInput.WorldPosition.ToVector2();
                var more = SpriteHierarchyRenderer.GetFromPool();
                more.Location = mousePosition;
                if (main == null)
                {
                    main = more;

                    obj.Renderers.Add(main);
                }
                else
                {
                    more.Location = Vector2.Transform(
                                  more.Location
                                  , Matrix.Invert(main.LocalTransform)
                              );
                    main.Children.Add(more);
                }
            };
            return obj;
        }
    }
}
