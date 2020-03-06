using Microsoft.Xna.Framework.Graphics;

namespace MonogameFacade
{
    public abstract class Renderer
    {
        public abstract void Draw(SpriteBatch batchGui, SpriteBatch batch, GameObject Parent);
    }
}
