using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MonogameFacade
{
    public sealed class GameObject
    {
        public Point Location;
        public Point Velocity;
        public bool IsPassive;
        public readonly List<Collider> Colliders = null;
        public readonly List<Renderer> Renderers = null;
        public int Identifier;

        private static Pool<GameObject> Pool = new Pool<GameObject>();
        public static readonly Action NoUpdate = () => { };

        public Action Update = null;

        public override string ToString()
        {
            return Identifier.ToString();
        }

        public static GameObject GetFromPool()
        {
            return Pool.Get();
        }

        public GameObject()
        {
            Colliders = new List<Collider>();
            Renderers = new List<Renderer>();
            Update = NoUpdate;
        }

        public void ReturnToPool()
        {
            foreach (var collider in Colliders)
                collider.ReturnToPool();
            Colliders.Clear();

            foreach (var renderer in Renderers)
                renderer.ReturnToPool();
            Renderers.Clear();

            IsPassive = true;
            Velocity = Location = Point.Zero;
            Update = NoUpdate;
            Identifier = 0;

            Pool.Return(this);
        }
    }
}
