using UnityEngine;

namespace AAFrameWork.Unity.Debug
{
    public static partial class DebugDispatcher
    {
        public static void Log(string info)
        {
            UnityEngine.Debug.Log($"[{Time.frameCount}] {info}");
        }

        public static void LogError(string info)
        {
            UnityEngine.Debug.LogError($"[{Time.frameCount}] {info}");
        }

        public static void LogWarning(string info)
        {
            UnityEngine.Debug.LogWarning($"[{Time.frameCount}] {info}");
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