using Microsoft.Xna.Framework;
using System;

namespace MonogameFacade
{
    public abstract class GameObject
    {
        public Point Location;
        public Point Velocity;
        public bool IsPassive = true;
        public abstract void Update(BaseGame game);

        public Bag<Collider> Colliders = new Bag<Collider>();
        public Bag<Collider> PassiveColliders = new Bag<Collider>();
        public Bag<Renderer> Renderers = new Bag<Renderer>();
    }
}
