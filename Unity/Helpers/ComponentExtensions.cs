using UnityEngine;

namespace Helpers
{
    public static class ComponentExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponent<T>() ?? obj.AddComponent<T>();
        }
    }
}