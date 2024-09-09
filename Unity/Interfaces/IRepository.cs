using System;
using System.Collections.Generic;

namespace AAFramework.Unity.Identifiers
{
    public interface IRepository<T> where T : class
    {
        void RegisterObject(T obj);
        void UnRegisterObject(T obj);
        IEnumerable<T> GetObjectsByPredicate(Func<T, bool> predicate);
    }
}