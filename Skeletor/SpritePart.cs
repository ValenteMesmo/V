using Microsoft.Xna.Framework;
using MonogameFacade;

namespace Skeletor
{
    public class SpritePart : GameObject
    {
        public SpritePart(BaseGame game)
        {
            var sprite = new SpriteRenderer();
            sprite.Texture = game.Textures["btn"];
            sprite.Size = new Microsoft.Xna.Framework.Point(1000);
            Renderers.Add(sprite);
        }
    }
}
