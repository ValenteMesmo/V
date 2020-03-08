using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MonogameFacade
{
    public abstract class GameObject
    {
        public Point Location;
        public Point Velocity;
        public bool IsPassive = true;
        public List<Collider> Colliders = null;
        public List<Renderer> Renderers = null;

        public GameObject()
        {
            Colliders = new List<Collider>();
            Renderers = new List<Renderer>();
        }

        public virtual void Update(BaseGame game) { }
    }
}
