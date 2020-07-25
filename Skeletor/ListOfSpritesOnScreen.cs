using Microsoft.Xna.Framework;
using MonogameFacade;

namespace Skeletor
{
    //public class ListOfSpritesOnScreen : GameObject
    //{
    //    private animsadasd animsaidasd;
    //    Pool<GuiButton> Pool = null;

    //    public ListOfSpritesOnScreen(animsadasd animsaidasd)
    //    {
    //        this.animsaidasd = animsaidasd;
    //        Pool = new Pool<GuiButton>();
    //        Location = new Point(-600, -350);
    //    }

    //    public override void Update(MonogameFacade.Game game)
    //    {
    //        Renderers.Clear();
    //        Pool.Clear();

    //        if (animsaidasd.Sprite == null)
    //            return;

    //        var i = 0;
    //        foreach (var item in animsaidasd.Sprite.Children)
    //        {
    //            var Label = Pool.Get();
    //            Label.SetText("test " + ++i);
    //            Label.SetOffset( new Point(0, (i -1) * 50));
    //            Renderers.AddRange(Label.Renderers);
    //        }
    //    }
    //}
}
