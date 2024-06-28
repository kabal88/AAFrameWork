using System.Collections.Generic;

namespace Helpers
{
    public static class CollectionExtensions
    {
        public static T RandomElement<T>(this T[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }
        
        public static T RandomElement<T>(this List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
        
        public static T[] Clone<T>(this T[] array)
        {
            var clone = new T[array.Length];
            array.CopyTo(clone, 0);
            return clone;
        }
        
    }
    
    public static class CustomExtensions
    {
        public static bool IsAlive(this IAlive obj)
        {
            return obj != null && obj.IsAlive;
        }
    }
}