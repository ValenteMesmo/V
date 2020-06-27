using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MonogameFacade
{

    //public abstract class Poolable
    //{
    //    public abstract void ReturnToPool();
    //}

    //TODO: where T is disposable
    //https://stackoverflow.com/questions/151051/when-should-i-use-gc-suppressfinalize
    //https://stackoverflow.com/questions/51350911/should-i-assume-that-every-owned-instance-implements-idisposable
    //https://stackoverflow.com/questions/538060/proper-use-of-the-idisposable-interface
    public class Pool<T> where T : new()
    {
        private readonly Queue<T> AvailableItems;

        public Pool()
        {
            AvailableItems = new Queue<T>();
        }

        public int AvailableCount => AvailableItems.Count;

        public T Get()
        {
            if (AvailableItems.Count == 0)
                return new T();

            return AvailableItems.Dequeue();
        }

        public void Return(T item)
        {
#if DEBUG
            if (item == null)
                throw new ArgumentNullException(nameof(item));
#endif

            AvailableItems.Enqueue(item);
        }

        public void Clear()
        {
            AvailableItems.Clear();
        }
    }

    public class Collider 
    {
        private static Pool<Collider> Pool = new Pool<Collider>();

        public GameObject Parent = null;
        public Rectangle Area;

        public int RelativeX => Parent.Location.X + Area.X;
        public int RelativeY => Parent.Location.Y + Area.Y;
        public int Width => Area.Width;
        public int Height => Area.Height;

        public CollisionHandler Handler = CollisionHandler.Empty;

        [Obsolete]
        public Collider() { }

        public void ReturnToPool()
        {
            Parent = null;
            Area = Rectangle.Empty;
            Handler = CollisionHandler.Empty;

            Pool.Return(this);
        }

        public static Collider GetFromPool()
        {
            return Pool.Get();
        }
    }
}
