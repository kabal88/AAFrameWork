using System;
using System.Collections.Generic;
using System.Linq;
using AAFramework.Unity.Identifiers;

namespace Services
{
    public sealed class UpdateLocalService : IUpdateService
    {
        private readonly List<IUpdatable> updatables;

        private Queue<IUpdatable> addUpdatables;
        private Queue<IUpdatable> removeUpdatables;

        public bool IsAlive { get; private set; }

        public UpdateLocalService()
        {
            updatables = new List<IUpdatable>();
            addUpdatables = new Queue<IUpdatable>();
            removeUpdatables = new Queue<IUpdatable>();
            IsAlive = true;
        }

        public void RegisterObject(IUpdatable updatable)
        {
            addUpdatables.Enqueue(updatable);
        }

        public void UnRegisterObject(IUpdatable updatable)
        {
            removeUpdatables.Enqueue(updatable);
        }

        public IEnumerable<IUpdatable> GetObjectsByPredicate(Func<IUpdatable, bool> predicate)
        {
            return updatables.Where(predicate);
        }

        public bool TryGetObject(out IUpdatable obj)
        {
            obj = updatables.FirstOrDefault();
            return obj != null;
        }
        
        public void UpdateLocal()
        {
            for (int i = 0; i < addUpdatables.Count; i++)
            {
                updatables.Add(addUpdatables.Dequeue());
            }

            for (int i = 0; i < removeUpdatables.Count; i++)
            {
                updatables.Remove(removeUpdatables.Dequeue());
            }
            
            for (var i = 0; i < updatables.Count; i++)
            {
                if (updatables[i].IsAlive)
                {
                    updatables[i].UpdateLocal();
                }
            }
        }
    }
}