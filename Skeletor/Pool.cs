using System;
using System.Collections.Generic;

namespace Skeletor
{
    public class Pool<T> where T : IPoolable
    {
        private readonly Queue<T> Available = null;
        private readonly Queue<T> Unavailable = null;

        public Pool(int size = 9)
        {
            Available = new Queue<T>(size);
            Unavailable = new Queue<T>(size);

            for (int i = 0; i < size; i++)
            {
                Available.Enqueue(Activator.CreateInstance<T>());
            }
        }

        public void Clear()
        {
            for (int i = 0; i < Unavailable.Count; i++)
            {
                var a = Unavailable.Dequeue();
                a.Reset();
                Available.Enqueue(a);
            }
        }

        internal T Get()
        {
            if (Available.Count == 0)
            {
                var a = Unavailable.Dequeue();
                a.Reset();
                Available.Enqueue(a);
            }

            var result = Available.Dequeue();
            Unavailable.Enqueue(result);
            return result;
        }
    }
}
