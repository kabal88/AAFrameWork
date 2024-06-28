using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;

namespace Services
{
    public sealed class FixUpdateLocalService : IFixUpdateService
    {
        private readonly List<IFixUpdatable> fixUpdatables;
        
        private Queue<IFixUpdatable> addFixUpdatables;
        private Queue<IFixUpdatable> removeFixUpdatables;
        
        public bool IsAlive { get; private set; }

        public FixUpdateLocalService()
        {
            fixUpdatables = new List<IFixUpdatable>();
            addFixUpdatables = new Queue<IFixUpdatable>();
            removeFixUpdatables = new Queue<IFixUpdatable>();
            IsAlive = true;
        }

        public void RegisterObject(IFixUpdatable obj)
        {
            addFixUpdatables.Enqueue(obj);
        }

        public void UnRegisterObject(IFixUpdatable obj)
        {
            removeFixUpdatables.Enqueue(obj);
        }

        public IEnumerable<IFixUpdatable> GetObjectsByPredicate(Func<IFixUpdatable, bool> predicate)
        {
            return fixUpdatables.Where(predicate);
        }
        
        public void FixedUpdateLocal()
        {
            for (var i = 0; i < addFixUpdatables.Count; i++)
            {
                fixUpdatables.Add(addFixUpdatables.Dequeue());
            }

            for (var i = 0; i < removeFixUpdatables.Count; i++)
            {
                fixUpdatables.Remove(removeFixUpdatables.Dequeue());
            }

            for (var i = 0; i < fixUpdatables.Count; i++)
            {
                if (fixUpdatables[i].IsAlive)
                {
                    fixUpdatables[i].FixedUpdateLocal();
                }
            }
        }
    }
}