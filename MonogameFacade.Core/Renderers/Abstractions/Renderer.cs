using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonogameFacade
{
    public abstract class Renderer 
    {
        public abstract void Draw(SpriteBatch batchGui, SpriteBatch batch, GameObject Parent);

        public abstract void ReturnToPool();
    }
}
