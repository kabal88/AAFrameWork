using System.IO;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace KabalFramework.Debug
{
    public partial class CheatWindow : OdinEditorWindow
    {
        [MenuItem("Game/Cheats")]
        public static void GetCheatWindow()
        {
            GetWindow<CheatWindow>();
        }
    
 
    }
}