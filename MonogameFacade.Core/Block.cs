using Microsoft.Xna.Framework;

namespace MonogameFacade
{
    public class Dpad : GameObject
    {
        public Dpad(BaseGame game)
        {
            var sprite = new GuiSpriteRenderer();
            sprite.Texture = game.Textures["shadedDark04"];
            sprite.Size = new Point(150, 150);
            //sprite.Offset = new Point(-1400, -750);
            sprite.Offset = new Point(-550, -300);
            //sprite.Offset = new Point(-350, -150);1
            Renderers.Add(sprite);

        }

        public override void Update(BaseGame game)
        {
            Location = game.GuiCamera.Location;
        }
    }

    public class Block : GameObject
    {
        public Block(BaseGame game)
        {
            var sprite = new SpriteRenderer();
            sprite.Texture = game.Textures["btn"];
            sprite.Size = new Point(100, 100);
            Renderers.Add(sprite);

            var collider = new Collider(this);
            collider.Area = new Rectangle(Point.Zero, sprite.Size);
            Colliders.Add(collider);
        }

        public override void Update(BaseGame game)
        {
          
        }
    }
}
