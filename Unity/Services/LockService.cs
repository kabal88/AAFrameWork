using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;

namespace Services
{
    public abstract class LockService<T> : IRepository<T>, IUpdatable where T : class, ILocker
    {
        protected readonly List<T> Lockers = new();
        protected readonly Queue<T> AddLockers = new();
        protected readonly Queue<T> RemoveLockers = new();
        
        public abstract bool IsLocked { get; }

        public abstract bool IsAlive { get; }

        public virtual void RegisterObject(T locker)
        {
            AddLockers.Enqueue(locker);
        }

        public virtual void UnRegisterObject(T locker)
        {
            RemoveLockers.Enqueue(locker);
        }

        public virtual IEnumerable<T> GetObjectsByPredicate(Func<T, bool> predicate)
        {
            return Lockers.Where(predicate);
        }

        public virtual void UpdateLocal(float deltaTime)
        {
            for (int i = 0; i < AddLockers.Count; i++)
            {
                Lockers.Add(AddLockers.Dequeue());
            }

            for (int i = 0; i < RemoveLockers.Count; i++)
            {
                Lockers.Remove(RemoveLockers.Dequeue());
            }
        }
    }
}