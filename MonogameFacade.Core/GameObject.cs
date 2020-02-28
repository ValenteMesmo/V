using Microsoft.Xna.Framework;
using System;

namespace MonogameFacade
{
    public abstract class GameObject
    {
        public Point Position;
        public abstract void Update(BaseGame game);
    }
}
