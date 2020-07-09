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
        private static readonly Pool<Collider> Pool = new Pool<Collider>();

        public GameObject Parent = null;
        public Rectangle Area;

        public int RelativeX => Parent.Location.X + Area.X;
        public int RelativeY => Parent.Location.Y + Area.Y;
        public int Width => Area.Width;
        public int Height => Area.Height;
        public Rectangle RelativeArea =>
            new Rectangle(
                Area.X + Parent.Location.X
                , Area.Y + Parent.Location.Y
                , Area.Width
                , Area.Height
            );

        //public CollisionHandler Handler = CollisionHandler.Empty;

        public Action<Collider, Collider> TopCollisionHandler;
        public Action<Collider, Collider> LeftCollisionHandler;
        public Action<Collider, Collider> BotCollisionHandler;
        public Action<Collider, Collider> RightCollisionHandler;
        public Action BeforeCollisionHandler;


        [Obsolete]
        public Collider()
        {
            Reset();
        }

        private static readonly Action<Collider, Collider> DefaultCollision = (a, b) => { };
        private static readonly Action DefaultBeforeCollision = () => { };

        public void ReturnToPool()
        {
            Reset();

            Pool.Return(this);
        }

        private void Reset()
        {
            Parent = null;
            Area = Rectangle.Empty;

            BeforeCollisionHandler = DefaultBeforeCollision;

            TopCollisionHandler =
                BotCollisionHandler =
                LeftCollisionHandler =
                RightCollisionHandler =
                DefaultCollision;
        }

        public static Collider GetFromPool()
        {
            return Pool.Get();
        }
    }
}
