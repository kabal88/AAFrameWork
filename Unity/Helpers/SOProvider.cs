using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class SOProvider<T> where T : UnityEngine.Object
{
    /// <summary>
    /// this is editor only code,  dont run it runtime
    /// </summary>
    /// <returns></returns>
    public IEnumerable<T> GetCollection()
    {
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
Debug.LogError("dont use soProvider runtime");
#endif

#if UNITY_EDITOR
        var containers = AssetDatabase.FindAssets($"t: {typeof(T).Name}")
            .Select(x => UnityEditor.AssetDatabase.GUIDToAssetPath(x))
            .Select(x => UnityEditor.AssetDatabase.LoadAssetAtPath<T>(x)).ToList();

        return containers;
#endif

        return default;
    }
}
