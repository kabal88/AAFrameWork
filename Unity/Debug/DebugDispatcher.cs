using UnityEngine;

namespace AAFrameWork.Unity.Debug
{
    public static class DebugDispatcher
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
    }
}