using MonogameFacade;

namespace Skeletor
{
    public class BonePart : GameObject
    {
        public BonePart(SpriteLineRenderer renderer)
        {            
            Renderers.Add(renderer);
        }
    }
}
