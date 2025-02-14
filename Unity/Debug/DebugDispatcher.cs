using UnityEngine;

namespace AAFrameWork.Unity.Debug
{
    public static partial class DebugDispatcher
    {
        public static void Log(string info)
        {

#if UNITY_EDITOR
            var message = $"[{Time.frameCount}] {info}";
#else
            var message = $"{info}";
#endif

            UnityEngine.Debug.Log(message);
        }

        public static void LogError(string info)
        {
#if UNITY_EDITOR
            var message = $"[{Time.frameCount}] {info}";
#else
            var message = $"{info}";
#endif
            UnityEngine.Debug.LogError(message);
        }

        public static void LogWarning(string info)
        {
#if UNITY_EDITOR
            var message = $"[{Time.frameCount}] {info}";
#else
            var message = $"{info}";
#endif
            UnityEngine.Debug.LogWarning(message);
        }

        public static void Log(string info, object sender)
        {
            Log($"{sender}: {info}");
        }

        public static void LogError(string info, object sender)
        {
            LogError($"{sender}: {info}");
        }

        public static void LogWarning(string info, object sender)
        {
            LogWarning($"{sender}: {info}");
        }
    }
}