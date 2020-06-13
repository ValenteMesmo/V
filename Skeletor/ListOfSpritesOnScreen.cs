using Microsoft.Xna.Framework;
using MonogameFacade;

namespace Skeletor
{
    public class ListOfSpritesOnScreen : GameObject
    {
        private animsadasd animsaidasd;
        Pool<GuiLabel> Pool = null;

        public ListOfSpritesOnScreen(animsadasd animsaidasd)
        {
            this.animsaidasd = animsaidasd;
            Pool = new Pool<GuiLabel>();
            Location = new Point(-600, -350);
        }

        public override void Update(BaseGame game)
        {
            Renderers.Clear();
            Pool.Clear();

            if (animsaidasd.Sprite == null)
                return;

            var i = 0;
            foreach (var item in animsaidasd.Sprite.Children)
            {
                var Label = Pool.Get();
                Label.SetText("test " + ++i);
                Label.SetOffset( new Point(0, (i -1) * 120));
                Renderers.AddRange(Label.Renderers);
            }
        }
    }
}
